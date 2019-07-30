let arr = [
    {
        id:1,
        name:"Коврик м555",
        description:"kjkjkjkjkjjnnnnn",
        url: "model1.jpg"
        
    },
    {
        id:2,
        name:"Коврик м556",
        description:"kjkjkjkjkjjnnnnn",
        url: "model2.jpg"
        
    },
    {
        id:3,
        name:"Коврик м557",
        description:"kjkjkjkjkjjnnnnn",
        url: "model3.jpg"
        
    },
    {
        id:4,
        name:"Коврик м558",
        description:"kjkjkjkjkjjnnnnn",
        url: "model4.jpg"
        
    },
];

let EllArr = [];
arr.forEach((el) => {
    var img = $(`<img src=${el.url}>`).addClass("frameImg");
    var domEl = $(`
    <div>
    </div>`).addClass("frame").append(img);
    $(".container").append(domEl);
});

