const inputBox=document.getElementById("input_box");
const listContainer=document.getElementById("list_container");
function addTask(){
    if(inputBox.value === ''){
        alert("Write Something !!")
    }
    else{
        let tasks= document.createElement("li");
        tasks.innerHTML =inputBox.value;
        listContainer.appendChild(tasks);
        let close_sign=document.createElement("span");
        close_sign.innerHTML="\u00d7";
        tasks.appendChild(close_sign);
    }
    inputBox.value="";

}
listContainer.addEventListener("click",function(event){
    if(event.target.tagName ==="LI"){
        event.target.classList.toggle("checked");
    }
    else if(event.target.tagName==="SPAN"){
        event.target.parentElement.remove();
    }
});