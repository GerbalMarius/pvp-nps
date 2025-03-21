function selectNPS(value, name) {
    document.querySelectorAll('.rating-btn').forEach(btn => {
        btn.classList.remove('active');
    });
    
    document.querySelector(`.rating-btn[data-value="${value}"]`).classList.add('active');
    
    document.getElementById(name).value = value;
}