self.addEventListener('fetch', event => {
    const url = new URL(event.request.url);

    // Example condition to handle a specific URL
    if (url.origin === 'https://sitescan.plus') {
        event.respondWith(
            fetch(event.request, { redirect: 'follow' })
                .then(response => {
                    // Handle the response as needed
                    return response;
                })
                .catch(error => {
                    // Handle the error as needed
                    return new Response('Network error', { status: 500 });
                })
        );
    } else {
        // Default fetch behavior for other requests
        event.respondWith(fetch(event.request));
    }
});
