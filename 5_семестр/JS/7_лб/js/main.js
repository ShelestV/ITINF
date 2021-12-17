const storage = window.localStorage;
let isInit = false;

let page = 0;
const numberOfElementsInPage = 8;

let sortByPriceDirection = "undefined";
let checkInStock = "undefined";

let minPrice = 0;
let maxPrice = 0;
let filterInStock = "undefined";

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

function thisPage() {
    let items = this.getItems((page - 1) * numberOfElementsInPage, numberOfElementsInPage);
    this.displayData(items);
}

function filter() {
    minPrice = document.getElementById('minPrice').value;
    maxPrice = document.getElementById('maxPrice').value;

    filterInStock = checkInStock;

    this.showData();
}

function changeInStokeFilter() {
    if (checkInStock === "undefined") {
        checkInStock = "in stock";
        document.getElementById('changeInStokeFilter').innerText = "In stock";
    } else if (checkInStock === "in stock") {
        checkInStock = "out of stock";
        document.getElementById('changeInStokeFilter').innerText = "Out of stock";
    } else if (checkInStock === "out of stock") {
        checkInStock = "undefined";
        document.getElementById('changeInStokeFilter').innerText = "All";
    }
}

function sortByPrice() {
    if (sortByPriceDirection === "undefined" || sortByPriceDirection === "asc") {
        this.updateData(this.sort(this.getData(), "price", false));
        sortByPriceDirection = "desc";
    } else if (sortByPriceDirection === "desc") {
        this.updateData(this.sort(this.getData(), "price", true));
        sortByPriceDirection = "asc";
    }
    this.thisPage();
}

function sort(data, byThat, sortDirection) {
    if (byThat === "price") {
        let prices = [];
        data.forEach((good) => {
            prices.push(good.price);
        });

        return this.sortByArray(data, prices, sortDirection);
    }

    return data;
}

function sortByArray(data, sortArray, sortDirection) {
    for (var i = 0, endI = sortArray.length - 1; i < endI; i++) {
        for (var j = 0, endJ = endI - i; j < endJ; j++) {
            let condition = sortDirection ? sortArray[j] > sortArray[j + 1] : sortArray[j] < sortArray[j + 1]
            if (condition) {
                let swap = sortArray[j];
                sortArray[j] = sortArray[j + 1];
                sortArray[j + 1] = swap;

                let swapObject = data[j];
                data[j] = data[j + 1];
                data[j + 1] = swapObject;
            }
        }
    }

    return data;
}

function showData() {
    page = 1;
    let items = this.getItems(0, numberOfElementsInPage);
    this.displayData(items);
}

function displayData(data) {
    document.getElementById("content").src = "../js/main.html";
    let html = document.getElementById("content");
    html.innerHTML = "";
    data.forEach(item => {
        html.innerHTML += `<div class="content-box">
                                <img class="content-box-image" src="${item.imagePath}" alt="image">
                                <div class="content-box-info">${item.info}</div>
                                <div class="content-box-type">${item.type}</div>
                                <div class="content-box-price">$${item.price}</div>
                                ${item.inStock ?
                                    `<button class="content-box-to-basket" onclick="addToBasket(${item.id})">To basket</button>` :
                                    `<div class="content-box-in-stock">Out of stock</div>`
                                }
                            </div>`;
    });
}

function basketToConsole() {
    console.log(this.getBasket());
}

function addToBasket(id) {
    let good = this.getDataById(id);
    let basket = this.getBasket();
    basket.push(good);
    updateBasket(basket);
}

function getBasket() {
    return JSON.parse(storage.getItem('basket'));
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

function updateData(data) {
    storage.setItem('data', JSON.stringify(data));
}

function updateBasket(basket) {
    storage.setItem('basket', JSON.stringify(basket));
}

function getMaxPrice(data) {
    let max = data[0].price;
    for (let i in data) {
        if (max < data[i].price) {
            max = data[i].price;
        }
    }
    return max;
}

function getData() {
    if (!isInit) {
        init();
    }

    let data = JSON.parse(localStorage.getItem('data'));
    let checkFilteredData = [];

    if (filterInStock === "undefined") {
        checkFilteredData = data;
    } else if (filterInStock === "in stock") {
        data.forEach((good) => {
            if (good.inStock) {
                checkFilteredData.push(good);
            }
        });
    } else if (filterInStock === "out of stock") {
        data.forEach((good) => {
            if (!good.inStock) {
                checkFilteredData.push(good);
            }
        });
    }

    let displayData = [];
    if (minPrice != -1 && maxPrice != -1) {
        checkFilteredData.forEach((good) => {
            if (minPrice <= good.price && good.price <= maxPrice) {
                displayData.push(good);
            }
        });
    } else {
        displayData = checkFilteredData;
    }

    return displayData;
}

function getDataById(id) {
    let data = this.getData();
    let result = {};
    data.forEach((good) => {
        if (good.id === id) {
            result = good;
        }
    });
    return result;
}

function init() {
    let data = [
        {
            id: 1,
            info: 'Aloe Vera<br/>In Mini Dolores Planter',
            type: 'Mini',
            price: 28,
            imagePath: 'Images/airplant3.jpg',
            inStock: true
        },
        {
            id: 2,
            info: 'Some plant<br/>Some text',
            type: 'Mini',
            price: 29,
            imagePath: 'Images/Aloe-Vera5_3.jpg',
            inStock: true
        },
        {
            id: 3,
            info: 'Name<br/>Try to add more text then previos one. I think it is true)',
            type: 'Not mini',
            price: 13,
            imagePath: 'Images/cactus-1.jpg',
            inStock: true
        },
        {
            id: 4,
            info: 'Text in one line without slash n',
            type: 'None',
            price: 10,
            imagePath: 'Images/cactus-18.jpg',
            inStock: true
        },
        {
            id: 5,
            info: 'You ask me to imagine some original info and I did it',
            type: 'Done',
            price: 19,
            imagePath: 'Images/gorshok-1.jpg',
            inStock: true
        },
        {
            id: 6,
            info: 'If I inderstood task correctly<br/>I should do pages',
            type: 'Question',
            price: 23,
            imagePath: 'Images/is3.jpg',
            inStock: true
        },
        {
            id: 7,
            info: 'Firstly I check this information',
            type: 'Check',
            price: 44,
            imagePath: 'Images/th4.jpg',
            inStock: true
        },
        {
            id: 8,
            info: 'Yes, I should implement pages with pictures and info for these pictures',
            type: 'Checked',
            price: 35,
            imagePath: 'Images/the-sill_1.jpg',
            inStock: true
        },
        {
            id: 9,
            info: 'I have any ideas what I should write in these descriptions',
            type: 'Doubts',
            price: 48,
            imagePath: 'Images/the-sill_2.jpg',
            inStock: true
        },
        {
            id: 10,
            info: 'Doing this lab I learn how to make adaptive page by css',
            type: 'Success',
            price: 33,
            imagePath: 'Images/Копия airplant3.jpg',
            inStock: true
        },
        {
            id: 11,
            info: 'Maybe in this case I can copy previos messages and paste them?',
            type: 'Question',
            price: 44,
            imagePath: 'Images/airplant3.jpg',
            inStock: true
        },
        {
            id: 12,
            info: 'Aloe Vera<br/>In Mini Dolores Planter',
            type: 'Mini',
            price: 28,
            imagePath: 'Images/airplant3.jpg',
            inStock: true
        },
        {
            id: 13,
            info: 'No, I don\'t)',
            type: 'Reject',
            price: 34,
            imagePath: 'Images/Aloe-Vera5_3.jpg',
            inStock: true
        },
        {
            id: 14,
            info: 'Of course pictures have to be copied',
            type: 'Sure',
            price: 28,
            imagePath: 'Images/airplant3.jpg',
            inStock: true
        },
        {
            id: 15,
            info: 'I\'m too lazy to search pictures, so I decide to copy and paste',
            type: 'CopyPaste',
            price: 7,
            imagePath: 'Images/cactus-1.jpg',
            inStock: true
        },
        {
            id: 16,
            info: 'There is left 7 pictures and I will finished with this writting',
            type: 'Doubts',
            price: 48,
            imagePath: 'Images/cactus-18.jpg',
            inStock: true
        },
        {
            id: 17,
            info: 'Oops... I did it again',
            type: 'Lady Gaga',
            price: 500,
            imagePath: 'Images/gorshok-1.jpg',
            inStock: false
        },
        {
            id: 18,
            info: 'Yes, I added a part of sogn of Lady Gaga...',
            type: 'Success',
            price: 44,
            imagePath: 'Images/is3.jpg',
            inStock: false
        },
        {
            id: 19,
            info: 'In the morning I fixed bug the reason for which I was(',
            type: 'Sadness',
            price: 28,
            imagePath: 'Images/th4.jpg',
            inStock: false
        },
        {
            id: 20,
            info: 'On Friday I fixed exception throwing, but when I was trying to fix one I change another file and some tests fall down',
            type: 'Falling',
            price: 34,
            imagePath: 'Images/the-sill_1.jpg',
            inStock: false
        },
        {
            id: 21,
            info: 'Vlad was helping me on Friday but we search in wrong place',
            type: 'One more fail',
            price: 28,
            imagePath: 'Images/the-sill_2.jpg',
            inStock: false
        },
        {
            id: 22,
            info: 'But today I\'ve gotten I f**ed up',
            type: 'Awareness',
            price: 7,
            imagePath: 'Images/Копия airplant3.jpg',
            inStock: false
        }
    ];
    this.updateData(data);
    this.updateBasket([]);
    
    document.getElementById('minPrice').value = minPrice;
    maxPrice = this.getMaxPrice(data);
    document.getElementById('maxPrice').value = maxPrice;

    isInit = true;
}