window.scrollThumbnailIntoView = (url) => {
    const img = Array.from(document.querySelectorAll('.thumbnail')).find(i => i.src.endsWith(url));
    if (img) img.scrollIntoView({ behavior: 'smooth', inline: 'center' });
};
