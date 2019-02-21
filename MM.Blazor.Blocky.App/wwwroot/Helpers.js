window.pixel = {
  getOffset: function (element) {
    return [element.offsetLeft, element.offsetTop];
  },
  getPixels: function (img, scalex, scaley) {

    if (!img.canvas) {
      img.canvas = document.createElement('canvas');
    }
    const canvas = img.canvas;
    const c2d = canvas.getContext('2d');
    canvas.width = scalex;
    canvas.height = scaley;
    const hRatio = canvas.width / img.width;
    const vRatio = canvas.height / img.height;
    const ratio = Math.min(hRatio, vRatio);
    c2d.clearRect(0, 0, canvas.width, canvas.height);
    const newWidth = parseInt(img.width * ratio);
    const newHeight = parseInt(img.height * ratio);
    c2d.drawImage(img, 0, 0, img.width, img.height,
      0, 0, newWidth, newHeight);
    const data = c2d.getImageData(0, 0, newWidth, newHeight).data;
    console.log(newWidth, newHeight);
    var result = new Array(newHeight);
    var pos = 0;
    for (var y = 0; y < newHeight; y++) {
      var row = new Array(newWidth);
      for (var x = 0; x < newWidth; x++) {
        row[x] = [data[pos], data[pos + 1], data[pos + 2], data[pos + 3]];
        pos += 4;
      }
      result[y] = row;
    }
    return result;
  }
};