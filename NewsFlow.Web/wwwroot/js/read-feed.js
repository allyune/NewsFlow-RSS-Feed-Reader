﻿const loadingContainer = document.querySelector('.loading-container');
const articleContainer = document.querySelector('.articles-container');

async function reloadArticles() {
    articleContainer.classList.add('visibility-none')
    loadingContainer.classList.remove('visibility-none');
    loadArticles();
}

async function loadArticles() {
    try {
        const feedId = getFeedId();
        const response = await fetch(`/api/feeds/${feedId}/articles`);
        if (!response.ok) {
            if (response.status === 400 || response.status === 404) {
                articleContainer.innerHTML = 'Feed not found'
                loadingContainer.classList.add('visibility-none');
                articleContainer.classList.remove('visibility-none');
            } else {
                throw new Error(`Unexpected response: ${data.status} ${data.statusText}`);
            }
        }
        else {
            var data = await response.json()
            displayArticles(data.value);
        }
    } catch (error) {
        handleLoadError(error);
    }
}

function displayArticles(data) {
    articleContainer.innerHTML = ' ';
    for (const article of data) {
        const div = createArticleElement(article);
        articleContainer.appendChild(div);
    }
    loadingContainer.classList.add('visibility-none');
    articleContainer.classList.remove('visibility-none');
}

function createArticleElement(article) {
    var div = document.createElement('div');
    //title
    var title = document.createElement('a');
    title.href = article.links[0];
    title.target = '_blank';
    title.innerHTML = article.title;
    div.appendChild(title);
    //published info
    var published = document.createElement('p');
    published.classList.add('pubinfo')
    var author = article.authors[0];
    var pubdate = article.pubDate;
    published.innerHTML = `Published: ${pubdate} by ${author}`
    div.appendChild(published);
    //summary
    var summary = document.createElement('p');
    summary.innerHTML = article.summary;
    div.appendChild(summary);
    //categories
    var categories = document.createElement('div');
    categories.classList.add("article-categories");
    for (var cat of article.categories.slice(0, 3)) {
        catName = document.createElement('p');
        catName.classList.add('article-category')
        catName.innerHTML = cat;
        categories.appendChild(catName);
    }
    div.appendChild(categories);
    return div;
}

function handleLoadError(error) {
    console.error('There was a problem while fetching articles:', error);
    const articleContainer = document.querySelector('.articles-container');
    articleContainer.innerHTML = "There was an error loading articles. Please try again.";
}

function getFeedId() {
    var currentUrl = window.location.href;
    var urlSegments = currentUrl.split('/');
    var lastSegment = urlSegments[urlSegments.length - 1];
    var lastSegmentParts = lastSegment.split('?');
    return lastSegmentParts[0];
}

async function feedUnsubscribe() {
    var feedId = getFeedId();
    var confirmUnsubscribe = window.confirm(
        "Are you sure you want to unsubscribe from this feed?");
    if (confirmUnsubscribe) {
        await deleteFeeds([feedId]);
        window.location.href = '/';
    }
}

function initReadFeed() {
    loadArticles();
    var reloadButton = document.querySelector('.btn-reload');
    reloadButton.addEventListener('click', reloadArticles);
    var unsubscribeButton = document.querySelector('#btn-unsubscribe');
    unsubscribeButton.addEventListener('click', feedUnsubscribe)
};


window.addEventListener('load', initReadFeed);

