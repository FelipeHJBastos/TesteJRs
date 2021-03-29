function dateToDateTime(date) {
    var day = date.getDate();       
    var month = date.getMonth() + 1;    
    var year = date.getFullYear();  
    var hour = date.getHours();      
    var minute = date.getMinutes(); 
    var second = date.getSeconds(); 

    return day + "/" + month + "/" + year + " " + hour + ':' + minute + ':' + second;
}
