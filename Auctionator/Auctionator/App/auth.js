export default function getIsAuth() {
    return !!localStorage.getItem('user');
};