export function getUloga() {
    let uloga = localStorage.getItem("uloga");
    if(uloga === null || uloga === undefined) {
        uloga = "";
        setUloga(uloga);
    }
    return uloga;
}

export function setUloga(uloga) {
    localStorage.setItem("uloga", uloga);
}

export function isKupacOnly() {
    return getUloga() === "Kupac";
}

export function isKupac() {
    return isAdminRadnik() ? true : getUloga() === "Kupac";
}

export function isAdminRadnik() {
    return isInvestitor() ? true :  getUloga() === "AdminRadnik";
}

export function isInvestitor() {
    return isAdmin() ? true :  getUloga() === "Investitor";
}

export function isAdmin() {
    return getUloga() === "Admin";
}