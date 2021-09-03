import axios from "axios";
import { LOGIN_FAIL, LOGIN_REQUEST, LOGIN_SUCCESS, SIGNUP_FAIL, SIGNUP_REQUEST, SIGNUP_SUCCESS } from "./types";

export const login = (credentials) => (dispatch) => {
  dispatch({ type: LOGIN_REQUEST });

  axios
    .post(
      "https://api.oasisplatform.world/api/avatar/authenticate",
      credentials,
      {
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
    .then((res) => {
      dispatch({ type: LOGIN_SUCCESS, payload: res.data });
      console.log(res)
    })
    .catch((err) => {
      dispatch({ type: LOGIN_FAIL, payload: err });
    });
};

export const signup = (credentials) => (dispatch) => {
    dispatch({ type: SIGNUP_REQUEST });
  
    axios
      .post(
        "https://api.oasisplatform.world/api/avatar/authenticate",
        credentials,
        {
          headers: {
            "Content-Type": "application/json",
          },
        }
      )
      .then((res) => {
        dispatch({ type: SIGNUP_SUCCESS, payload: res.data });
        console.log(res)
      })
      .catch((err) => {
        dispatch({ type: SIGNUP_FAIL, payload: err });
      });
  };
