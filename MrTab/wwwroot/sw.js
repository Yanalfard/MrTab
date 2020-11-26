const staticCacheName = 'site-static-v2';
const dynamicCacheName = 'site-dynamic-v2';
const cacheMaxSize = 100;

//const assets = [
//    '/',
//    '/manifest.json',
//    '/Scripts/app.js',
//    '/css/croppie.css',
//    '/css/uikit.min.css',
//    '/css/Global.min.css',
//    '/css/Controls.min.css',
//    '/css/Index.min.css',
//    '/fonts/IRANYekan.ttf'
//];

const assets = [];

//install the service worker
self.addEventListener('install', (evt) => {
    evt.waitUntil(
        caches.open(staticCacheName).then(cache => {
            for (let item of assets) {
                cache.add(item);
            }
            //cache.addAll(assets);
        })
    )
});

// cache size limit 
const limitCacheSize = (name, size) => {
    caches.open(name).then(cache => {
        cache.keys().then(keys => {
            if (keys.length > size) {
                cache.delete(keys[0]).then(limitCacheSize(name, size));
            }
        })
    })
}

//activate service worker
self.addEventListener('activate', (evt) => {
    //console.log('Service worker activated');
    evt.waitUntil(
        caches.keys().then(keys => {
            return Promise.all(keys
                .filter(key => key !== staticCacheName && key !== dynamicCacheName)
                .map(key => caches.delete(key)))
        })
    )
});

self.addEventListener('fetch', (evt) => {
    //console.log('Fetch event', evt);
    evt.respondWith(
        caches.match(evt.request).then(cacheRes => {
            if (!evt.request.url.includes('http')) return fetch(evt.request);
            return cacheRes || fetch(evt.request).then(fetchRes => {
                return caches.open(dynamicCacheName).then(cache => {
                    cache.put(evt.request.url, fetchRes.clone());
                    limitCacheSize(dynamicCacheName, cacheMaxSize);
                    return fetchRes;
                })
            }).catch(() => {
                if (evt.request.url.includes('/Home/') || evt.request.url.includes('/View/'))
                    return caches.match('/fallback.html')
            });
        })
    )
})