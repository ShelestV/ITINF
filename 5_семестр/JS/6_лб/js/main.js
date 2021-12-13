let page = 0;
const numberOfElementsInPage = 8;

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

    document.getElementById('loadDataButton').style.display = 'none';

    document.getElementById('previosPageButton').style.visibility = 'visible';
    document.getElementById('nextPageButton').style.visibility = 'visible';
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
                            </div>`;
    });
}

function getItems(startIndex, length) {
console.clear();

    let data = this.getData();
    let items = [];

    for (let i in data) {
        console.log(`startIndex(${startIndex}) <= i(${i}) && i(${i}) < length(${startIndex + length}) = ${startIndex <= i && i < startIndex + length}`);
        if (startIndex <= i && i < startIndex + length) {
            items.push(data[i]);
        }
    }

    return items;
}

function getData() {
    return [
        {
            info: 'Aloe Vera<br/>In Mini Dolores Planter',
            type: 'Mini',
            price: 28,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'Some plant<br/>Some text',
            type: 'Mini',
            price: 29,
            imagePath: 'Images/Aloe-Vera5_3.jpg'
        },
        {
            info: 'Name<br/>Try to add more text then previos one. I think it is true)',
            type: 'Not mini',
            price: 13,
            imagePath: 'Images/cactus-1.jpg'
        },
        {
            info: 'Text in one line without slash n',
            type: 'None',
            price: 10,
            imagePath: 'Images/cactus-18.jpg'
        },
        {
            info: 'You ask me to imagine some original info and I did it',
            type: 'Done',
            price: 19,
            imagePath: 'Images/gorshok-1.jpg'
        },
        {
            info: 'If I inderstood task correctly<br/>I should do pages',
            type: 'Question',
            price: 23,
            imagePath: 'Images/is3.jpg'
        },
        {
            info: 'Firstly I check this information',
            type: 'Check',
            price: 44,
            imagePath: 'Images/th4.jpg'
        },
        {
            info: 'Yes, I should implement pages with pictures and info for these pictures',
            type: 'Checked',
            price: 35,
            imagePath: 'Images/the-sill_1.jpg'
        },
        {
            info: 'I have any ideas what I should write in these descriptions',
            type: 'Doubts',
            price: 48,
            imagePath: 'Images/the-sill_2.jpg'
        },
        {
            info: 'Doing this lab I learn how to make adaptive page by css',
            type: 'Success',
            price: 33,
            imagePath: 'Images/Копия airplant3.jpg'
        },
        {
            info: 'Maybe in this case I can copy previos messages and paste them?',
            type: 'Question',
            price: 44,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'Aloe Vera<br/>In Mini Dolores Planter',
            type: 'Mini',
            price: 28,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'No, I don\'t)',
            type: 'Reject',
            price: 34,
            imagePath: 'Images/Aloe-Vera5_3.jpg'
        },
        {
            info: 'Of course pictures have to be copied',
            type: 'Sure',
            price: 28,
            imagePath: 'Images/airplant3.jpg'
        },
        {
            info: 'I\'m too lazy to search pictures, so I decide to copy and paste',
            type: 'CopyPaste',
            price: 7,
            imagePath: 'Images/cactus-1.jpg'
        },
        {
            info: 'There is left 7 pictures and I will finished with this writting',
            type: 'Doubts',
            price: 48,
            imagePath: 'Images/cactus-18.jpg'
        },
        {
            info: 'Oops... I did it again',
            type: 'Lady Gaga',
            price: 500,
            imagePath: 'Images/gorshok-1.jpg'
        },
        {
            info: 'Yes, I added a part of sogn of Lady Gaga...',
            type: 'Success',
            price: 44,
            imagePath: 'Images/is3.jpg'
        },
        {
            info: 'In the morning I fixed bug the reason for which I was(',
            type: 'Sadness',
            price: 28,
            imagePath: 'Images/th4.jpg'
        },
        {
            info: 'On Friday I fixed exception throwing, but when I was trying to fix one I change another file and some tests fall down',
            type: 'Falling',
            price: 34,
            imagePath: 'Images/the-sill_1.jpg'
        },
        {
            info: 'Vlad was helping me on Friday but we search in wrong place',
            type: 'One more fail',
            price: 28,
            imagePath: 'Images/the-sill_2.jpg'
        },
        {
            info: 'But today I\'ve gotten I f**ed up',
            type: 'Awareness',
            price: 7,
            imagePath: 'Images/Копия airplant3.jpg'
        }
    ]
}