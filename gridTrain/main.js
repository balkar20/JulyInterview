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
        url: "model5.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "model6.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "model7.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "model8.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "model9.jpg",
        price: 50

    },
    {
        id: 4,
        name: "м558",
        description: "kjkjkjkjkjjnnnnn",
        url: "model10.jpg",
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