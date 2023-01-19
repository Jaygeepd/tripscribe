import {StorageTypes} from "../constants";

const getLocalStorage = (identifier: StorageTypes) => {
    const auth = localStorage.getItem(identifier);
    if (!auth) return null;

    return JSON.parse(auth);
};

const setLocalStorage = (value: any, identifier: StorageTypes) => {
    if (value) {
        localStorage.setItem(identifier, JSON.stringify(value));
    }
};

const removeLocalStorage = (identifier: StorageTypes) => {
    localStorage.removeItem(identifier);
};

export default {
    getLocalStorage,
    setLocalStorage,
    removeLocalStorage,
};