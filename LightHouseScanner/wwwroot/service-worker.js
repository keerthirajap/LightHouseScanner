self.addEventListener('fetch', event => {
    event.respondWith(
        fetch(event.request, { redirect: 'follow' })
            .then(response => {
                if (!response.ok) {
                    console.error('Fetch error:', response.statusText);
                    return new Response('Network error', { status: 500 });
                }
                return response;
            })
            .catch(error => {
                console.error('Fetch error:', error);
                return new Response('Network error', { status: 500 });
            })
    );
});
