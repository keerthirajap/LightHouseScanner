self.addEventListener('install', (event) => {
    console.log('Service worker: Install');
    event.waitUntil(
        caches.open('v1').then((cache) => {
            return cache.addAll([
                // List your assets here
                '/',
                '/index.html',
                '/styles.css',
                '/script.js',
                // etc.
            ]);
        })
    );
});

self.addEventListener('activate', (event) => {
    console.log('Service worker: Activate');
    event.waitUntil(
        caches.keys().then((cacheNames) => {
            return Promise.all(
                cacheNames.map((cache) => {
                    if (cache !== 'v1') {
                        console.log('Service worker: Clearing old cache');
                        return caches.delete(cache);
                    }
                })
            );
        })
    );
});

self.addEventListener('fetch', (event) => {
    event.respondWith(
        fetch(event.request, { redirect: 'follow' })
            .then((response) => {
                // Check if we received a valid response
                if (!response || response.status !== 200 || response.type !== 'basic') {
                    return response;
                }

                // Clone the response
                const responseToCache = response.clone();

                caches.open('v1')
                    .then((cache) => {
                        cache.put(event.request, responseToCache);
                    });

                return response;
            })
            .catch((error) => {
                console.error('Fetching failed:', error);
                return caches.match(event.request)
                    .then((response) => {
                        return response || fetch(event.request);
                    });
            })
    );
});
