const selectBtn = document.querySelector('#selectBtn')
const actionsBtn = document.querySelector('#actionsBtn')
const actionsPopup = document.querySelector('#actionsPopup')

const toggled = 'toggled'

let toggleSelectBtn = () => {
    if (selectBtn.classList.contains(toggled)) {
        selectBtn.classList.remove(toggled)
    } else {
        selectBtn.classList.add(toggled)
    }
}

selectBtn.onclick = toggleSelectBtn

let showActionsPopup = () => {
    if (!actionsPopup.classList.contains(toggled)) {
        actionsPopup.classList.add(toggled)
    }
}

actionsBtn.onclick = showActionsPopup

document.addEventListener("click", function(event) {
    if (event.target !== actionsBtn && !actionsBtn.contains(event.target)
        && event.target !== actionsPopup && !actionsPopup.contains(event.target)) {
        actionsPopup.classList.remove(toggled);
    }
});

const createScopeBtn = document.querySelector('#createScopeBtn')
const createScopeDialog = document.querySelector('#createScopeDialog')

createScopeBtn.onclick = () => {
    createScopeDialog.showModal()
}