const SET_USER = "SET_USER"
const SET_FETCH_ERROR = "SET_FETCH_ERROR"
const LOGOUT = "LOGOUT"

const defaultState = {
    currentUser: {},
    isAuth: false,
    isFetchError : false,
}

export default function userReducer(state = defaultState, action) {
    switch (action.type) {
        case SET_USER:
            return {
                ...state,
                currentUser: action.payload,
                isAuth: true
            }
        case SET_FETCH_ERROR:
            return {
                ...state,
                isFetchError: action.payload,
            }
        case LOGOUT:
            localStorage.removeItem('token')
            return {
                ...state,
                currentUser: {},
                isAuth: false
            }
        default:
            return state
    }
}


export const setUser = user => ({type: SET_USER, payload: user})
export const setFetchError = (bool) => ({type: SET_FETCH_ERROR, payload: bool})
export const logout = () => ({type: LOGOUT})