const display =document.getElementById("display");

function addToDisplay(input){
    display.value +=input;
}
function clearDisplay(){
    display.value ="";

}
function calculate(){
    display.value =eval(display.value);
}
function backspace(){
        display.value = display.value.slice(0,-1);
    }
