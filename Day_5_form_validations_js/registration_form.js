function setError(errorSpan,message){
    let submibutton =document.getElementById("submitButton")
    errorSpan.className='errorSpan';
    errorSpan.innerText =message;
    submibutton.disabled=true;
}

function setSuccess(errorSpan){
    let submibutton =document.getElementById("submitButton")
    errorSpan.className='successSpan';
    errorSpan.innerText ="";
    submibutton.disabled=false;
}

// Validate Room No

function validateRoomNo(){
    const roomNoInput = document.getElementById('room_no');
    const errorSpan = document.getElementById('roomNoError');
    if (/^\d+-\d+$/ .test(roomNoInput.value)){
        setSuccess(errorSpan);
        return true
    }
    else if(roomNoInput.value.trim()===''){
        setError(errorSpan,"Room Number is required");
    }
    else{
        setError(errorSpan,"Please enter in the format ROOM-BED,");
        return false
    }
    
}

// validate firstname

function validateFirstname(){
    const firstnameInput = document.getElementById('firstname');
    const errorSpan = document.getElementById('firstnameError');

    const alphaRegex = /^[A-Za-z\s]+$/;
    if(firstnameInput.value.trim()===''){
        setError(errorSpan,"First name is required");
    }
    else if (!alphaRegex.test(firstnameInput.value.trim())) {
        setError(errorSpan,"First name can only contain letters and spaces.");
       
    } else {
        setSuccess(errorSpan);
        
    }
}



// validate secondname

function validateLastname(){
    const lastnameInput = document.getElementById('lastname');
    const errorSpan = document.getElementById('lastnameError');
    const alphaRegex = /^[A-Za-z\s]+$/;

    if(lastnameInput.value.trim()===''){
        setError(errorSpan,"Last name is required");
        return false
    }
    else if (!alphaRegex.test(lastnameInput.value.trim())) {
        setError(errorSpan,"Last name can only contain letters and spaces.");
        return false
    } else {
        setSuccess(errorSpan);
        return true
    }
}

// validation for passport number 1

function validatePassportNo() {
    const passportNoInput = document.getElementById('passport_no');
    const errorSpan = document.getElementById('passportError');

    const passportRegex = /^[A-Za-z0-9]{6,9}$/;

    if (passportRegex.test(passportNoInput.value.trim())){
        setSuccess(errorSpan);
    }
    else if(passportNoInput.value.trim()===''){
        setError(errorSpan,"Passport number is required");
    }
    else{
        setError(errorSpan,'Please enter a valid passport number (6-9 alphanumeric characters).');
    }
}
// validation for passport2


function validatePassportNo2() {
    const passportNoInput = document.getElementById('passport_no2');
    const errorSpan = document.getElementById('passportError2');
    const passportRegex = /^[A-Za-z0-9]{6,9}$/;

    if (passportRegex.test(passportNoInput.value.trim())){
        setSuccess(errorSpan);
    }
    else{
        setError(errorSpan,'Please enter a valid passport number (6-9 alphanumeric characters).'); 
    }
}

// validate days of stay

function validateLengthofStay(){
    const numberofDays=document.getElementById('lengthofstay');
    const errorSpan =document.getElementById('lengthofstayError');

    const numberofDaysinput=parseInt(numberofDays.value,10);

    if(isNaN(numberofDaysinput) || numberofDaysinput<=0){
        setError(errorSpan,'Please enter a valid no of days'); 
    }
    else{
        setSuccess(errorSpan);
    }
}

// validation of date of birth


function validateBirthdate(){

    let submitbutton =document.getElementById("submitButton");
    const monthInput = document.getElementById('birthMonth');
    const errorSpan = document.getElementById('dobError');
    const birthdayinput =document.getElementById('birthDay');
    const yearInput =document.getElementById('birthYear');
    const month= parseInt(monthInput.value,10);
    const birthday= parseInt(birthdayinput.value,10);
    const year= parseInt(yearInput.value,10);
    const currentYear = new Date().getFullYear();

    if(isNaN(month) || isNaN(birthday) || isNaN(year)){
        errorSpan.textContent='Invalid input';
        submitbutton.disabled=true;
        return false;
    }
    else if(month<1 || month>12){
        errorSpan.textContent='Please enter a valid month';
        submitbutton.disabled=true;
        return false;
    }
    else if(birthday<1 || birthday>31){
        errorSpan.textContent='Please enter a valid day';
        submitbutton.disabled=true;
        return false;

    }
    else if(year < 1900 || year > currentYear){
        errorSpan.textContent='Please enter a valid year';
        submitbutton.disabled=true;
        return false;

    }
    
    else{
        errorSpan.textContent='';
        return true
    }
}

document.getElementById('birthMonth').addEventListener('input',validateBirthdate);
document.getElementById('birthDay').addEventListener('input',validateBirthdate);
document.getElementById('birthYear').addEventListener('input',validateBirthdate);

// validation of date of expiry

function validateExpirydate(){
    let submitbutton =document.getElementById("submitButton");
    const monthInput = document.getElementById('expiryMonth');
    const errorSpan = document.getElementById('doeError');
    const dayinput =document.getElementById('expiryDay');
    const yearInput =document.getElementById('expiryYear');
    const month= parseInt(monthInput.value,10);
    const day= parseInt(dayinput.value,10);
    const year= parseInt(yearInput.value,10);
    const currentYear = new Date().getFullYear();

    if(isNaN(month) || isNaN(day) || isNaN(year)){
        errorSpan.textContent='Invalid input';
        submitbutton.disabled=true;
        return false;
    }
    else if(month<1 || month>12){
        errorSpan.textContent='Please enter a valid month';
        submitbutton.disabled=true;
        return false;
    }
    else if(day<1 || day>31){
        errorSpan.textContent='Please enter a valid day';
        submitbutton.disabled=true;
        return false;

    }
    else if(year < 1900 || year > 2050){
        errorSpan.textContent='Please enter a valid year';
        submitbutton.disabled=true;
        return false;

    }
    
    else{
        errorSpan.textContent='';
        return true
    }
}

document.getElementById('expiryMonth').addEventListener('input',validateExpirydate);
document.getElementById('expiryDay').addEventListener('input',validateExpirydate);
document.getElementById('expiryYear').addEventListener('input',validateExpirydate);



// validation of passport expiry 2

function validateExpirydate2(){
    let submitbutton =document.getElementById("submitButton");
    const monthInput = document.getElementById('expiryMonth2');
    const errorSpan = document.getElementById('doeError2');
    const dayinput =document.getElementById('expiryDay2');
    const yearInput =document.getElementById('expiryYear2');
    const month= parseInt(monthInput.value,10);
    const day= parseInt(dayinput.value,10);
    const year= parseInt(yearInput.value,10);
    const currentYear = new Date().getFullYear();

    if(isNaN(month) || isNaN(day) || isNaN(year)){
        errorSpan.textContent='Invalid input';
        submitbutton.disabled=true;
        return false;
    }
    else if(month<1 || month>12){
        errorSpan.textContent='Please enter a valid month';
        submitbutton.disabled=true;
        return false;
    }
    else if(day<1 || day>31){
        errorSpan.textContent='Please enter a valid day';
        submitbutton.disabled=true;
        return false;

    }
    else if(year < 1900 || year > 2050){
        errorSpan.textContent='Please enter a valid year';
        submitbutton.disabled=true;
        return false;

    }
    
    else{
        errorSpan.textContent='';
        return true
    }
}

document.getElementById('expiryMonth2').addEventListener('input',validateExpirydate2);
document.getElementById('expiryDay2').addEventListener('input',validateExpirydate2);
document.getElementById('expiryYear2').addEventListener('input',validateExpirydate2);

// validation for date of arrival

function validateArrivaldate(){
    let submitbutton =document.getElementById("submitButton");
    const monthInput = document.getElementById('arrivalMonth');
    const errorSpan = document.getElementById('arrivalError');
    const dayinput =document.getElementById('arrivalDay');
    const yearInput =document.getElementById('arrivalYear');
    const month= parseInt(monthInput.value,10);
    const day= parseInt(dayinput.value,10);
    const year= parseInt(yearInput.value,10);
    const currentYear = new Date().getFullYear();
    const timeInput = document.getElementById('arrivalTime');
    const timeRegex = /^([01]\d|2[0-3]):([0-5]\d)$/;
    const time = timeInput.value.trim();

    if(isNaN(month) || isNaN(day) || isNaN(year)){
        errorSpan.textContent='Invalid input';
        submitbutton.disabled=true;
        return false;
    }
    else if(month<1 || month>12){
        errorSpan.textContent='Please enter a valid month';
        submitbutton.disabled=true;
        return false;
    }
    else if(day<1 || day>31){
        errorSpan.textContent='Please enter a valid day';
        submitbutton.disabled=true;
        return false;

    }
    else if(year < 1900 || year > currentYear){
        errorSpan.textContent='Please enter a valid year';
        submitbutton.disabled=true;
        return false;

    }
    else if (!timeRegex.test(time)) {
        errorSpan.textContent = 'Please enter a valid time in the format HH:MM (24-hour clock).';
        submitbutton.disabled=true;
        return false;
    }
    
    else{
        errorSpan.textContent='';
        return true
    }
}

document.getElementById('arrivalMonth').addEventListener('input',validateArrivaldate);
document.getElementById('arrivalDay').addEventListener('input',validateArrivaldate);
document.getElementById('arrivalYear').addEventListener('input',validateArrivaldate);
document.getElementById('arrivalTime').addEventListener('input', validateArrivaldate);


// submit validations

function validateForm() {
    const roomNoInput = document.getElementById('room_no');
    const firstnameInput = document.getElementById('firstname');
    const passportNoInput = document.getElementById('passport_no');
    const numberofDays=document.getElementById('lengthofstay');
    if(roomNoInput.value.trim()==='' || firstnameInput.value.trim()==='' || passportNoInput.value.trim()===''  ||  numberofDays.value.trim()==='' ){
        alert("Please fill the all the inputs");
        return false;
    }
    else{
        alert("succesfully submitted");
        return true;
    }  
}