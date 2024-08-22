

//email validation

$(document).ready(function() {
    $('#email').on('input',function(){
        const email =$(this).val().trim();
        const messageContainer=$('#emailMessage')
       
        if (!validateEmail(email)) {
            $("#email").addClass("inputError");
            messageContainer.text('Please enter a valid email address');
        } else {
            $("#email").removeClass("inputError");
            messageContainer.text('');
        }
    });
    function validateEmail(email){
        const emailPattern=/^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailPattern.test(String(email).toLowerCase());

    
    }
})

// password validation

$(document).ready(function() {
    $('#password').on('input', function() {
        const password = $(this).val().trim();
        const messageContainer = $('#passwordMessage');

        if (!validatePassword(password)) {
            $("#password").addClass("inputError");
            messageContainer.text('Password must be at least 5 characters long and contain at least one number and one symbol.');
        } else {
            $("#password").removeClass("inputError");
            messageContainer.text('');
        }
    });

    function validatePassword(password) {
        const minLength = 5;
        const hasNumber = /\d/;          
        const hasSymbol = /[!@#$%^&*(),.?":{}|<>]/; 
        return password.length >= minLength && hasNumber.test(password) && hasSymbol.test(password);
    }
});

// floating label

$(document).ready(function() {

    $(".inputfield").on("focusin",function(){
        $(this).parent().find("label").addClass("active");
    });
    $(".inputfield").on("focusout",function(){
        if($(this).val().trim()===""){
            $(this).parent().find("label").removeClass("active");
        }
    
    });
});
