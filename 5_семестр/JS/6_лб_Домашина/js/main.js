let page = 0;
const numberOfElementsInPage = 8;
let displayClass = "grid";

function changeDisplayType() {
    if (displayClass === "grid") {
        displayClass = "list";
        document.getElementById('content').className = 'list-content';
        document.getElementById('displayType').textContent = 'To grid';
    } else {
        displayClass = "grid";
        document.getElementById('content').className = 'grid-content';
        document.getElementById('displayType').textContent = 'To list';
    }

    this.showData();
    
}

function previosPage() {
    if (page > 1) {
        --page;

        let items = this.getItems((page - 1) * numberOfElementsInPage, numberOfElementsInPage);
        this.displayData(items);
    }
}

function nextPage() {
    ++page;
    let indexOnNextPage = (page - 1) * numberOfElementsInPage;
    if (indexOnNextPage < this.getData().length) {
        let items = this.getItems(indexOnNextPage, numberOfElementsInPage);
        this.displayData(items);
    } else {
        --page;
    }
}

function showData() {
    page = 1;
    let items = this.getItems(0, numberOfElementsInPage);
    this.displayData(items);
}

function displayData(data) {
    if (displayClass === "grid") {
        //document.getElementById("grid-content").src = "../js/main.html";
        let html = document.getElementById("content");
        html.innerHTML = "";
        data.forEach(item => {
            html.innerHTML += `<div class="grid-content-box">
                                    <img class="grid-content-box-image" src="${item.imagePath}" alt="image">
                                    <div class="grid-content-box-info">${item.info}</div>
                                    <div class="grid-content-box-type">${item.type}</div>
                                    <div class="grid-content-box-price">$${item.price}</div>
                                </div>`;
        });
    } else {
        //document.getElementById("list-content").src = "../js/main.html";
        let html = document.getElementById("content");
        html.innerHTML = "";
        data.forEach(item => {
            html.innerHTML += `<div class="list-content-box">
                                    <img class="list-content-box-image" src="${item.imagePath}" alt="image">
                                    <div class="list-content-box-info">${item.info}</div>
                                    <div class="list-content-box-type">${item.type}</div>
                                    <div class="list-content-box-price">$${item.price}</div>
                                </div>`;
        });
    }

    
}

function getItems(startIndex, length) {
    let data = this.getData();
    let items = [];

    for (let i in data) {
        if (startIndex <= i && i < startIndex + length) {
            items.push(data[i]);
        }
    }

    return items;
}

function getData() {
    return [
        {
            info: 'Air Plant',
            type: 'Mini',
            price: 37,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'Aloe Vera',
            type: 'Middle',
            price: 24,
            imagePath: 'Images/Aloe-Vera5_3.jpg'
        },
        {
            info: 'Cactus from Africa',
            type: 'Middle',
            price: 7,
            imagePath: 'Images/cactus-1.jpg'
        },
        {
            info: 'Cactus from store',
            type: 'Middle',
            price: 38,
            imagePath: 'Images/cactus-18.jpg'
        },
        {
            info: 'Flower pot',
            type: 'Empty',
            price: 15,
            imagePath: 'Images/gorshok-1.jpg'
        },
        {
            info: 'Orhid phalaenopsis',
            type: 'Maxi',
            price: 14,
            imagePath: 'Images/is3.jpg'
        },
        {
            info: 'Monstera',
            type: 'Maxi',
            price: 27,
            imagePath: 'Images/th4.jpg'
        },
        {
            info: 'Noua',
            type: 'Mini',
            price: 42,
            imagePath: 'Images/the-sill_1.jpg'
        },
        {
            info: 'Epipremnum Aureum',
            type: 'Doubts',
            price: 21,
            imagePath: 'Images/the-sill_2.jpg'
        },
        {
            info: 'Any little plants',
            type: 'Mini',
            price: 26,
            imagePath: 'Images/Копия airplant3.jpg'
        },
        {
            info: 'Any little plants copy',
            type: 'Mini',
            price: 34,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'Air plant',
            type: 'Mini',
            price: 17,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'Aloe Vera<br/>In Mini Dolores Planter',
            type: 'Middle',
            price: 55,
            imagePath: 'Images/Aloe-Vera5_3.jpg'
        },
        {
            info: 'One more Air Plant',
            type: 'Mini',
            price: 10,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'Simple cactus',
            type: 'Middle',
            price: 15,
            imagePath: 'Images/cactus-1.jpg'
        },
        {
            info: 'Cereus repandus',
            type: 'Middle',
            price: 35,
            imagePath: 'Images/cactus-18.jpg'
        },
        {
            info: 'Gorshok',
            type: 'Empty',
            price: 12,
            imagePath: 'Images/gorshok-1.jpg'
        },
        {
            info: 'Orchid phalaenopsis',
            type: 'Middle',
            price: 30,
            imagePath: 'Images/is3.jpg'
        },
        {
            info: 'Monstera one more time',
            type: 'Maxi',
            price: 25,
            imagePath: 'Images/th4.jpg'
        },
        {
            info: 'Big Noua',
            type: 'Middle',
            price: 12,
            imagePath: 'Images/the-sill_1.jpg'
        },
        {
            info: 'House epipremnum aureum',
            type: 'Maxi',
            price: 37,
            imagePath: 'Images/the-sill_2.jpg'
        },
        {
            info: 'Decorative plants',
            type: 'Mini',
            price: 21,
            imagePath: 'Images/Копия airplant3.jpg'
        }
    ]
}