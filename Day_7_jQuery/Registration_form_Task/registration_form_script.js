$(document).ready(function () {
    $(function() {
        $( "#datepicker" ).datepicker({
            maxDate: 0,  
            yearRange: "-100:+0" 
        });
    });
    // tooltip initialisation
    $('input').tooltip({
        disabled: true, 
     })
    // firstname validation
    $('#firstname').on('input', function () {
        const firstName = $(this).val().trim();
        if (!validateName(firstName)) {
            $(this).addClass('inputError');
            $(this).attr('title', 'Only characters are allowed, and the field cannot be empty.').tooltip('enable').tooltip('open');
            updateProgress(1, false);
        } else {
            $(this).removeClass('inputError');
            $(this).attr('title', '').tooltip('disable');
            updateProgress(1, true);
        }
    });
    // lastname validation
    $('#lastname').on('input', function () {
        const lastName = $(this).val().trim();
        const messageContainer = $('#lastnameError');
        if (!validateName(lastName)) {
            $(this).addClass('inputError');
            $(this).attr('title', 'Only characters are allowed, and the field cannot be empty.').tooltip('enable').tooltip('open');
            updateProgress(1, false);
        } else {
            $(this).removeClass('inputError');
            $(this).attr('title', '').tooltip('disable');
            updateProgress(1, true);
        }
    });
    function validateName(name) {
        const namePattern = /^[A-Za-z]+$/;
        return namePattern.test(name);
    }
//    validate phonenumber
    $('#phone').on('input', function () {
        const phoneNumber = $(this).val().trim();
        const messageContainer = $('#phonenumberError');

        if (!validatePhoneNumber(phoneNumber)) {
            $(this).addClass('inputError');
            $(this).attr('title', 'Please enter a valid 10-digit phone number').tooltip('enable').tooltip('open');
            updateProgress(2, false);
        } else {
            $(this).removeClass('inputError');
            $(this).attr('title', '').tooltip('disable');
            updateProgress(2, true);
        }
    });
    function validatePhoneNumber(phoneNumber) {
        const phonePattern = /^[6-9]\d{9}$/;
        return phonePattern.test(phoneNumber);
    }
    // validate Email
    $('#email').on('input',function(){
        const email =$(this).val().trim();
        const messageContainer=$('#emailError')
        if (!validateEmail(email)) {
            $("#email").addClass("inputError");
            $(this).attr('title', 'Please enter a valid email address').tooltip('enable').tooltip('open');
        } else {
            $("#email").removeClass("inputError");
            $(this).attr('title', '').tooltip('disable');
        }
    });
    function validateEmail(email){
        const emailPattern=/^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        return emailPattern.test(String(email).toLowerCase());
    }
    // address validations
    $('#address').on('input', function () {
        const address = $(this).val().trim();
        if (address === '') {
            $(this).addClass('inputError');
            $(this).attr('title', 'Address cannot be empty').tooltip('enable').tooltip('open');
            updateProgress(3, false);
        } else {
            $(this).removeClass('inputError');
            $(this).attr('title', '').tooltip('disable');
            updateProgress(3, true);
        }
    });
    // usernamevalidations
    $('#username').on('input', function () {
        const userName = $(this).val().trim();

        if (!validateName(userName)) {
            $(this).addClass('inputError');
            $(this).attr('title', 'Only characters are allowed, and the field cannot be empty').tooltip('enable').tooltip('open');
            updateProgress(4, false);
        } else {
            $(this).removeClass('inputError');
            $(this).attr('title', '').tooltip('disable');
            updateProgress(4,true);
        }
    });
    // Password validations
    $(document).ready(function() {
        $('#password').on('input', function() {
            const password = $(this).val().trim();
            if (!validatePassword(password)) {
                $("#password").addClass("inputError");
                $(this).attr('title', 'Password must be at least 5 characters long and contain at least one number and one symbol.').tooltip('enable').tooltip('open');
            } else {
                $("#password").removeClass("inputError");
                $(this).attr('title', '').tooltip('disable');
            }
        });
        function validatePassword(password) {
            const minLength = 5;
            const hasNumber = /\d/;          
            const hasSymbol = /[!@#$%^&*(),.?":{}|<>]/; 
            return password.length >= minLength && hasNumber.test(password) && hasSymbol.test(password);
        }
    });
    // confirm password validation
    $('#confirmpassword').on('input', function () {
        const confirmPassword = $(this).val().trim();
        const password = $('#password').val().trim();
        if (confirmPassword !== password) {
            $(this).addClass('inputError');
            $(this).attr('title', 'Passwords do not match.').tooltip('enable').tooltip('open');
        } else {
            $(this).removeClass('inputError');
            $(this).attr('title', '').tooltip('disable');
        }
    });
    //   floating label
        $(".inputfield").on("focusin",function(){
            $(this).parent().find("label").addClass("active");
        });
        $(".inputfield").on("focusout",function(){
            if($(this).val().trim()===""){
                $(this).parent().find("label").removeClass("active");
            }
        });
    // city validation
    const citiesByState = {
        Kerala: ["Ernakulam", "Calicut", "Trivandrum"],
        Tamilnadu: ["Chennai", "Salem", "Coimbatore"],
    };
    $('#state').change(function() {
        const state = $(this).val();
        let citiesOptions = '<option value="">Select City</option>';
        if (state) {
            const cities = citiesByState[state];
            $.each(cities, function(index, city) {
                citiesOptions += '<option value="' + city+'">' + city + '</option>';
            });
            updateProgress(3,true);
        }
        else{
            updateProgress(3,false);
        }
        $('#city').html(citiesOptions);
    });    
    // Function to update the progress indicator
        function updateProgress(section, isActive) {
            var step = $('.progressStep[datasection="' + section + '"]');
            if (isActive) {
                step.addClass('activate');
            } else {
                step.removeClass('activate');
            }
        }




})
$(document).ready(function() {
    $('#signupForm').on('submit', function(e) {
        if (!validateForm()) {
            e.preventDefault(); // Prevent the form from submitting if validation fails
        }
    });
    function validateForm() {
        const firstnameInput = document.getElementById('firstname');
        const lastnameInput = document.getElementById('lastname');
        const phoneInput = document.getElementById('phone');
        const usernameInput = document.getElementById('username');
        const passwordInput = document.getElementById('password');
        const confirmPasswordInput=document.getElementById('confirmpassword');
        if(lastnameInput.value.trim()==='' || firstnameInput.value.trim()==='' || phoneInput.value.trim()===''  ||  usernameInput.value.trim()===''|| passwordInput.value.trim()==='' || confirmPasswordInput.value.trim()===''){
            alert("Please fill the all the inputs");
            return false;
        }
        else{
            alert("succesfully submitted");
            return true;
        }  
    }
});


    
