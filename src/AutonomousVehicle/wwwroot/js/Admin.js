export function elementExists(id) {
    return document.getElementById(id) !== null
}

export function hasClass(id, c) {
    return document.getElementById(id).classList.contains(c)
}

export function removeClass(id, c) {
    document.getElementById(id).classList.remove(c)
}

export function addClass(id, c) {
    document.getElementById(id).classList.add(c)
}