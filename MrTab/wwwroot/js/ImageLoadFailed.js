const images = document.getElementsByTagName('img');
for (let image of images) {
    image.setAttribute("onerror", "imageError(this)")
}

function imageError(image) {
    image.onerror = "";
    image.src = "/Resources/Svg/NoImage.svg";
    return true;
}