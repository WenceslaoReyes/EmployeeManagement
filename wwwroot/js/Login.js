let loginWrapper = document.querySelector('.WrapperLogin');
//let imageArr = [`linear-gradient(to right, #4ca1af, #c4e0e5)`,
//    `linear - gradient(to right, #4b79a1, #283e51)`,
//    `linear-gradient(to right, #5a3f37, #2c7744)`,
//    `linear-gradient(to right, #eacda3, #d6ae7b)`];
//let index = 0;
//setInterval(() => {
//    loginWrapper.style.backgroundImage = `${imageArr[index]}`;
//    index++;
//    if (index == imageArr.length) {
//        index = 0;
//    }
//},10000)

let imageArr = ["url('../img/photo-1.jpeg')", "url('../img/photo-2.jpeg')", "url('../img/photo-3.jpeg')", "url('../img/photo-4.jpeg')", "url('../img/photo-5.jpeg')"];

let index = 1;
setInterval(() => {
    loginWrapper.style.backgroundImage = `${imageArr[index]}`;
    index++;
    if (index == imageArr.length) {
        index = 0;
    }
},30000)