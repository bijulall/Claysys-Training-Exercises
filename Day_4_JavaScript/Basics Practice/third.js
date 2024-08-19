function firstFunction() {
    document.getElementById("demo3").innerHTML = "Hello Wolrd.";
  }

function secondFunction(){
    let x=5 ;
    let y=10 ;
    let z=x+y;
    document.getElementById("demo4").innerHTML=z;
}
{
const cars = ["Saab","Volvo","BMW"];

document.getElementById("demo5").innerHTML = cars[0];
}

let y = myFunction(4, 3);
document.getElementById("demo6").innerHTML = y;

function myFunction(a, b) {
  return a * b;
}

// Create an Object
const person = {
    firstName: "John",
    lastName: "Doe",
    age:50,
    eyeColor: "blue"
  };
  
  // Create a Copy
  const x = person;
  person.nationality="indian"
  
  // Change Age
  x.age = 10;
  
  document.getElementById("demo7").innerHTML =
  person.firstName + " is " + person.age + " years old.";

  document.getElementById("demo8").innerHTML =
  person.firstName + " is an " + person.nationality ;
// object method
  const man = {
    firstName: "John",
    lastName: "Doe",
    id: 5566,
    fullName: function() {
      return this.firstName + " " + this.lastName;
    }
  };
  
  document.getElementById("demo9").innerHTML = man.fullName();


  const fruits = ["Banana", "Orange", "Apple", "Mango"];
  document.getElementById("demo10").innerHTML = fruits.toString();
  

const element = document.getElementById("myBtn");
element.addEventListener("click",myClickFunction);

function myClickFunction(){
  document.getElementById("democlick").innerHTML="Hello New World";
}
let text = "";
let i = 0;
while (i < 10) {
  text += "<br>The number is " + i;
  i++;
}
document.getElementById("demo10").innerHTML = text;
{
const cars = ["BMW", "Volvo", "Saab", "Ford", "Fiat", "Audi"];

let text2 = "";
for (let i = 0; i < cars.length; i++) {
  text2 += cars[i] + "<br>";
}
document.getElementById("demo11").innerHTML = text2;
}