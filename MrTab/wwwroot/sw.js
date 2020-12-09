const staticCacheName = 'site-static-v0.0.1';
const pagesCacheName = 'site-pages-v0.0.1';
const isOnline = navigator.onLine;
const domain = self.origin;

const assets = [

];

const pages = ['/Test/GetRoutes', '/Test/Index', '/Entry/Login', '/Entry/Register', '/Entry/Validation', '/Entry/ForgotPassword', '/Entry/ChangePassword', '/Entry/CodeSent', '/Home/Index', '/Home/About', '/Home/Search', '/Home/Profile', '/Home/ToRestaurant', '/Restaurant/ViewSingle', '/User/Profile', '/RouteAnalyzer_Main/ShowAllRoutes']
//install the service worker
self.addEventListener('install', (evt) => {
    evt.waitUntil(
        caches.open(staticCacheName).then(cache => {
            for (let item of assets) {
                cache.add(item);
            }
        })
    )

});

//activate service worker
self.addEventListener('activate', (evt) => {
    evt.waitUntil(
        caches.keys().then(keys => {
            return Promise.all(keys
                .filter(key => key !== staticCacheName)
                .map(key => caches.delete(key)))
        })
    )
});

self.addEventListener('fetch', (evt) => {
    //self.clients.matchAll().then(a => console.log(a));

    let isPage = false;
    for (let route of pages) {
        if (evt.request.url.replace(domain, '').toLowerCase() === route.toLowerCase()) {
            console.log(route);
            isPage = true;
            break;
        }
    }

    //HERE IT IS
    //if (isPage) {
    //    if (isOnline) {
    //        caches.match(evt.request).then((cacheRes) => {
    //            return cacheRes || fetch(evt.request)
    //                .then(fetchRes => {

    //                    //if the url is a resource, cache it
    //                    return caches.open(pagesCacheName).then(cache => {
    //                        cache.put(evt.request.url, fetchRes.clone());

    //                        return fetchRes;
    //                    })

    //                    return fetchRes;
    //                })
    //        })
    //    }
    //}

    return;

    evt.respondWith(
        caches.match(evt.request)
            .then(cacheRes => {
                return cacheRes || fetch(evt.request)
                    .then(fetchRes => {

                        //if the url is a resource, cache it
                        if (evt.request.url.includes('/Styles/')
                            || evt.request.url.includes('/Scripts/')
                            || evt.request.url.includes('/Resources/')) {

                            return caches.open(staticCacheName).then(cache => {
                                cache.put(evt.request.url, fetchRes.clone());

                                return fetchRes;
                            })
                        }

                        return fetchRes;
                    })
            })
    )
})