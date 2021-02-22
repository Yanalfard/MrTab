function copy(text, message) {
    var textArea = document.createElement("textarea");
    textArea.value = text;

    // Avoid scrolling to bottom
    textArea.style.top = "0";
    textArea.style.left = "0";
    textArea.style.position = "fixed";

    document.body.appendChild(textArea);
    textArea.focus();
    textArea.select();

    try {
        var successful = document.execCommand('copy');
    } catch (err) {
    }

    document.body.removeChild(textArea);

    if (message === undefined || message === null) return;

    UIkit.notification(message);
}

let isInStandaloneMode = window.matchMedia('(display-mode: standalone)').matches;
const isSafari = /^((?!chrome|android).)*safari/i.test(navigator.userAgent);
const isIos = /iphone|ipad|ipod/.test(window.navigator.userAgent.toLowerCase());

let deferredPrompt;
const addButtons = document.getElementsByClassName('btn-pwa');

for (let btn of addButtons) {
    btn.addEventListener('click', btnDownloadClick);
}

window.addEventListener('beforeinstallprompt', (e) => {
    try {
        // Prevent Chrome 67 and earlier from automatically showing the prompt
        e.preventDefault();
        // Stash the event so it can be triggered later.
        deferredPrompt = e;

    } catch { }
});

function hidePwaButtons() {

    let ShouldHide = false;
    isInStandaloneMode = window.matchMedia('(display-mode: standalone)').matches;

    let ios_webview = false;
    if (navigator.platform.substr(0, 2) === 'iP') {
        if (window.indexedDB) {
            // WKWebView
            ios_webview = true;
        }
    }

    ShouldHide ||= isInStandaloneMode;

    try {
        ShouldHide ||= myWebView.getSettings().setUserAgentString("Android");
    } catch (e) { }

    ShouldHide ||= ios_webview;

    if (!ShouldHide) return;
    for (let addBtn of addButtons) {
        addBtn.style.display = "none";
    }

    for (let item of document.querySelectorAll('.hideOnStandalone')) {
        item.style.display = 'none';
    }

    const a = document.getElementsByClassName('parallax-download')[0];
    if (a === undefined) return;
    a.style.display = 'none';
}

setTimeout(() => {
    hidePwaButtons()
}, 200);

function btnDownloadClick(e) {

    if (isInStandaloneMode) {
        UIkit.notification('برنامه نصب شده است');
        return;
    }
    //Is not installed
    if (isSafari && isIos) {
        UIkit.modal(document.getElementById('Modal-PopupIos')).show();
        return;
    }

    try {
        deferredPrompt.prompt();
    } catch (e) {
        console.log(e);
        return;
    }

    // Wait for the user to respond to the prompt
    deferredPrompt.userChoice.then((choiceResult) => {
        if (choiceResult.outcome === 'accepted') {
            console.log('User accepted the A2HS prompt');
            hidePwaButtons();
            window.location.reload();
        } else {
            console.log('User dismissed the A2HS prompt');
        }
        deferredPrompt = null;
    });
}

if (isSafari && isIos) {
    if (localStorage.getItem('HasShownPrompt') !== 'shown') {
        setTimeout(() => {
            UIkit.modal(document.getElementById('Modal-PopupIos')).show();
            localStorage.setItem('HasShownPrompt', 'shown');

        }, 30000);
    }
}