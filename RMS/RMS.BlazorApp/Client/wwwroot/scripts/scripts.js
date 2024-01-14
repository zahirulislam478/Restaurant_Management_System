function elementClick(elt) {
    console.log('Element:', elt);
    if (elt) {
        elt.click();
    } else {
        console.error('Element is null or undefined.');
    }
}
