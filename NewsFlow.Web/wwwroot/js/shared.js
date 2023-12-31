﻿async function addFeed() {
    console.log("Adding a feed")
    const newFeedName = document.querySelector('#new-feed-name').value;
    const newFeedLink = document.querySelector('#new-feed-link').value;
    const status = document.querySelector('#modal-status');
    var response = await fetch('/api/feeds', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            name: newFeedName,
            link: newFeedLink,
        })
    });
    if (!response.ok) {
        status.innerHTML = await response.text();
    }
    else {
        status.innerHTML = " ";
        status.innerHTML = "Feed added successfully."
        location.reload();
    }
}

function initSharedScripts() {
    console.log('Shared scripts initialized');
    var addButton = document.querySelector('#btn-modal-add-feed');
    addButton.addEventListener('click', addFeed);
};


window.addEventListener('load', initSharedScripts);