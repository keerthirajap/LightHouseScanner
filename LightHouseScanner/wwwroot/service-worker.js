self.addEventListener('fetch', event => {
    event.respondWith(
        fetch(event.request, { redirect: 'follow' }).catch(error => {
            console.error('Fetch failed; returning offline page instead.', error);

            return caches.match('offline.html');
        })
    );
});