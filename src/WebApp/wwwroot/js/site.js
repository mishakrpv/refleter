const selectBtn = document.querySelector('#selectBtn')
    
let toggleSelectBtn = () => {
    let key = 'toggled'
    if (selectBtn.classList.contains(key)) {
        selectBtn.classList.remove(key)
    } else {
        selectBtn.classList.add(key)
    }
}

selectBtn.onclick = toggleSelectBtn