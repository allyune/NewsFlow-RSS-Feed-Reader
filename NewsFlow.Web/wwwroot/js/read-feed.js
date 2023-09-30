async function loadArticles() {
    try {
        const feedId = getFeedId();
        const data = await fetchArticles(feedId);
        displayArticles(data);
    } catch (error) {
        handleLoadError(error);
    }
}

async function fetchArticles(feedId) {
    const response = await fetch(`/api/feeds/${feedId}/articles`);
    if (!response.ok) {
        throw new Error('Bad response from API');
    }
    return await response.json();
}

function displayArticles(data) {
    const articleContainer = document.querySelector('.articles-container');
    articleContainer.innerHTML = ' ';
    for (const article of data.value) {
        const div = createArticleElement(article);
        articleContainer.appendChild(div);
    }
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

function showAddFeedModal() {
    console.log("Clicked on add feed button")
    var addFeedModal = document.getElementById('add-feed-modal');
    addFeedModal.modal('show');
}

function initReadFeed() {
    console.log("View initialized");
    loadArticles();
    var reloadButton = document.querySelector(".btn-reload");
    reloadButton.addEventListener('click', loadArticles);
    var addFeedButton = document.querySelector(".btn-add-feed");
    addFeedButton.addEventListener('click', showAddFeedModal)
};


window.addEventListener('load', initReadFeed);

