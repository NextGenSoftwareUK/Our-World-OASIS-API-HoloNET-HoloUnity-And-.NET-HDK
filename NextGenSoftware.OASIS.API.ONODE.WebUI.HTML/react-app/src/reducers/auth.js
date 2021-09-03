import { LOGIN_FAIL, LOGIN_REQUEST, LOGIN_SUCCESS, SIGNUP_FAIL, SIGNUP_REQUEST, SIGNUP_SUCCESS } from "../actions/types";

export const login = (state = { loading: true, isAuthenticated: false }, action) => {
  switch (action.type) {
    case LOGIN_REQUEST:
      return { loading: true, isAuthenticated: false };
    case LOGIN_SUCCESS:
      return {
        loading: false,
        data: action.payload,
        isAuthenticated: true,
      };
    case LOGIN_FAIL:
      return { loading: false, error: action.payload, isAuthenticated: false };
    default:
      return state;
  }
};

export const signup = (state = { loading: true }, action) => {
    switch (action.type) {
      case SIGNUP_REQUEST:
        return { loading: true };
      case SIGNUP_SUCCESS:
        return {
          loading: false,
          data: action.payload,
        };
      case SIGNUP_FAIL:
        return { loading: false, error: action.payload };
      default:
        return state;
    }
  };