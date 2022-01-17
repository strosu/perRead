import { useSetRecoilState } from 'recoil';

import { history } from '../_helpers/history';
import { useFetchWrapper } from '../_helpers/fetch-wrapper';
import { authAtom } from '../_state/auth';

export { useUserActions };

function useUserActions () {
    const fetchWrapper = useFetchWrapper();
    const setAuth = useSetRecoilState(authAtom);

    return {
        login,
        logout,
        getAll
    }

    function login(username, password) {
        return fetchWrapper.post(`https://localhost:7176/login`, { username, password })
            .then(user => {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('user', JSON.stringify(user));
                setAuth(user);

                // get return url from location state or default to home page
                const { from } = history.location.state || { from: { pathname: '/' } };
                history.push(from);
            });
    }

    function logout() {
        // remove user from local storage, set auth state to null and redirect to login page
        localStorage.removeItem('user');
        setAuth(null);
        history.push('/login');
    }

    function getAll() {
        // return fetchWrapper.get(baseUrl).then(setUsers);
    }    
}