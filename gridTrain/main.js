let arr = [{
        id: 1,
        name: "м555",
        description: "kjkjkjkjkjjnnnnn",
        url: "model1.jpg",
        price: 50
    },
    {
        id: 2,
        name: "м556",
        description: "kjkjkjkjkjjnnnnn",
        url: "model2.jpg",
        price: 50

    },
    {
        id: 3,
        name: "м557",
        description: "kjkjkjkjkjjnnnnn",
        url: "model3.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "model4.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "http://www.autowaysgroup.com/Content/ue/net/upload1/Other/110410/6361871073512862882090563.png",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "http://dk.autowaysgroup.com/Content/ue/net/upload1/Other/110410/6361869264611934511679960.png",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "http://dk.autowaysgroup.com/Content/ue/net/upload1/Other/110410/6361871061958608849663153.png",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "https://refaccionariamario.info/5374-large_default/juego-de-tapetes-de-alfombra-con-hule-con-emblema-rojo-de-vw.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "https://s1.medias-norauto.es/images_produits/alfombrillas-pvc-cancun/650x650/4-alfombrillas-coche-universales-de-pvc-cancun-color-carbono--761041.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "https://www.google.com/search?q=%D1%84%D0%BE%D1%82%D0%BE+%D0%BA%D0%BE%D0%B2%D1%80%D0%BE%D0%B2+%D0%B4%D0%BB%D1%8F+%D0%B0%D0%B2%D1%82%D0%BE&source=lnms&tbm=isch&sa=X&ved=0ahUKEwi3kZHYxt_jAhXDtYsKHfteAPIQ_AUIESgB&biw=1280&bih=625#imgdii=EyD-X6mHF8dSPM:&imgrc=-E5lSgBkI3G_EM:",
        price: 50

    },
];

let EllArr = [];
arr.forEach((el) => {
    var img = $(`<img src=${el.url}>`).addClass("frameImg");
    var info = $(`
    <div class="info">
        <span class="model">Модель: <span class="val">${el.name}</span></span>
        <span class="price">Цена: <span class="val">${el.price}</span></span>
        <a href="#" class="more">Подробнее..</a>
    </div>`);
    var domEl = $(`
    <div>
    </div>`).addClass("frame").append(img).append(info);
    $(".container").append(domEl);
});

// arr.forEach((el) => {
//     var imageContainer = $(`
//     <div>
//     </div>`).addClass("imgBox");
//     var img = $(`<img src=${el.url}>`).addClass("frameImg");

//     imageContainer.add(img);

//     var info = $(`
//     <div class="info">
//         <span class="model">Модель: <span class="val">${el.name}</span></span>
//         <span class="price">Цена: <span class="val">${el.price}</span></span>
//         <a href="#" class="more">Подробнее..</a>
//     </div>`);
//     var domEl = $(`
//     <div>
//     </div>`).addClass("frame").append(info).append(imageContainer);
//     $(".container").append(domEl);
