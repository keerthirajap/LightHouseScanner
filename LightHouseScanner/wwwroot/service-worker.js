self.addEventListener('fetch', event => {
    console.log('Fetch event for:', event.request.url);

    event.respondWith(
        fetch(event.request, { redirect: 'follow' })
            .then(response => {
                console.log('Fetch response status:', response.status, 'for URL:', event.request.url);
                if (!response.ok) {
                    console.error('Fetch error:', response.statusText);
                    return new Response('Network error', { status: 500 });
                }
                return response;
            })
            .catch(error => {
                console.error('Fetch error for URL:', event.request.url, 'Error:', error);
                return new Response('Network error', { status: 500 });
            })
    );
});
