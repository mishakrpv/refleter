const selectBtn = document.querySelector('#selectBtn')
const scopes = document.querySelector('#scopes')
const actionsBtn = document.querySelector('#actionsBtn')
const actionsPopup = document.querySelector('#actionsPopup')
const overlay = document.querySelector('.overlay')
const scopesExp = document.querySelector('#scopesExp')

const toggled = 'toggled'

let toggleSelectBtn = () => {
    if (selectBtn.classList.contains(toggled)) {
        selectBtn.classList.remove(toggled)
        scopesExp.classList.remove(toggled)
        scopes.style.display = 'none'
    } else {
        selectBtn.classList.add(toggled)
        scopesExp.classList.add(toggled)
        scopes.style.display = 'block'
    }
}

selectBtn.onclick = toggleSelectBtn

let showActionsPopup = () => {
    if (!actionsPopup.classList.contains(toggled)) {
        actionsPopup.classList.add(toggled)
    } else {
        actionsPopup.classList.remove(toggled)
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
    overlay.style.display = 'flex'
    createScopeDialog.showModal()
}

const createScopeCloseBtn = document.querySelector('#createScopeCloseBtn')
const createScopeCancelBtn = document.querySelector('#creationCancelBtn')

createScopeDialog.onclose = () => {
    overlay.style.display = 'none'
}

createScopeCancelBtn.onclick = () => {
    createScopeDialog.close()
}

createScopeCloseBtn.onclick = () => {
    createScopeDialog.close()
}

const scopeNameInput = document.querySelector('#scopeNameInput')
const scopeNameErrorMsg = document.querySelector('#snErrorMsg')
const createConfirmationBtn = document.querySelector('#creationConfirmBtn')

scopeNameInput.oninput = () => {
    let validityState = scopeNameInput.validity;
    let message = ''

    if (!validityState.valid) {
        if (validityState.valueMissing) {
            message = 'Empty field';
        } else {
            message = 'Value can’t be shorter than 3 characters';
        }
        scopeNameErrorMsg.style.display = 'block'
        scopeNameInput.classList.add('invalid')
        scopeNameErrorMsg.textContent = message
        createConfirmationBtn.classList.add('locked')
    } else {
        createConfirmationBtn.classList.remove('locked')
        scopeNameErrorMsg.style.display = 'none'
        scopeNameInput.classList.remove('invalid')
    }
}