
let buttonUl = document.querySelector(".button-menu ul");

let btnMenu = function (){
    buttonUl.style.left = 0;
    buttonUl.style.transition = "ease-in-out 250ms";
    buttonUl.style.opacity = "1";
}

let btnClose = function (){
    buttonUl.style.left = "-100%" ;
    buttonUl.style.transition = "ease-in-out 250ms";
    buttonUl.style.opacity = "0";

}

