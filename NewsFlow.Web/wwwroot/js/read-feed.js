﻿const loadingContainer = document.querySelector('.loading-container');
const articleContainer = document.querySelector('.articles-container');

async function reloadArticles() {
    articleContainer.classList.add('visibility-none')
    loadingContainer.classList.remove('visibility-none');
    loadArticles();
}

$(function () {
    $('#datepicker').daterangepicker({
        opens: 'left'
    }, function (start, end, label) {
        filterArticlesByDate(start, end);
    });
});

function filterArticlesByDate(startDate, endDate) {
    const articlesContainer = document.querySelector('.articles-container');
    const articles = articlesContainer.querySelectorAll('.article');

    articles.forEach(article => {
        var publishDateElement = article.querySelector('.pubdate');
        var publishDateStr = publishDateElement.innerHTML;
        var publishDate = new Date(publishDateStr);

        if (publishDate >= startDate && publishDate <= endDate) {
            article.style.display = 'block';
        } else {
            article.style.display = 'none';
        }
    });
}

async function loadArticles() {
    try {
        var feedId = getFeedId();
        var response = await fetch(`/api/feeds/${feedId}/articles`);
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
            displayLastUpdated(data.value.lastUpdated);
            displayArticles(data.value.articles);
        }
    } catch (error) {
        handleLoadError(error);
    }
}

function displayLastUpdated(date) {
    var lastUpdatedElement = document.querySelector('.feed-last-updated');
    var dateText;

    lastUpdatedElement.innerHTML = `Last updated: ${date}`;
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
    div.classList.add('article');
    //title
    var title = document.createElement('a');
    title.classList.add('article-title')
    title.href = article.links[0];
    title.target = '_blank';
    title.innerHTML = article.title;
    div.appendChild(title);
    //published info
    var published = document.createElement('div');
    published.classList.add('pubinfo');
    var pubdate = document.createElement('p');
    pubdate.classList.add('pubdate');
    pubdate.innerHTML = article.pubDate;
    published.appendChild(pubdate);
    var author = document.createElement('p');
    author.innerHTML = `by ${article.authors[0]}`;
    published.appendChild(author);
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

async function deleteFeeds(feeds) {
    var response = await fetch('/api/feeds', {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(feeds)
    });

    if (!response.ok) {
        alert(response.text)
    }
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

function findArticles(searchText) {
    var articles = document.querySelectorAll('.article');
    console.log(articles);
    for (var article of articles) {
        var title = article.querySelector('.article-title').textContent.toLowerCase();
        console.log(title);
        if (searchText && !title.includes(searchText)) {
            article.style.display = 'none';
        } else {
            article.style.display = '';
        }
    }
};

function initReadFeed() {
    loadArticles();
    var reloadButton = document.querySelector('.btn-reload');
    reloadButton.addEventListener('click', reloadArticles);
    var unsubscribeButton = document.querySelector('#btn-unsubscribe');
    unsubscribeButton.addEventListener('click', feedUnsubscribe);
    var searchInput = document.querySelector('#article-search-input');
    console.log(searchInput);
    searchInput.addEventListener('input', function () {
        var searchText = this.value.toLowerCase();
        console.log(searchText);
        findArticles(searchText)
    });
}


window.addEventListener('load', initReadFeed);

