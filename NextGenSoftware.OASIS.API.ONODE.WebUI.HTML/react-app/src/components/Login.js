import React from "react";
import axios from "axios";
import ShowIcon from "../assets/images/visible-icon.svg";
import HideIcon from "../assets/images/hidden-icon.svg";

import ForgetPassword from "./ForgetPassword";

import { ToastContainer, toast } from "react-toastify";

import { Modal } from "react-bootstrap";
import Loader from "react-loader-spinner";
import { Formik } from "formik";
import * as Yup from "yup";
import oasisApi from "oasis-api";

export default class Login extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: "",
      showPassword: false,
      showForgetPassword: false,
      loading: false,
      user: null,
    };
  }

  initialValues = {
    email: "",
    password: "",
  };
  validationSchema = Yup.object().shape({
    email: Yup.string().email("Email is invalid").required("Email is required"),
    password: Yup.string()
      .required("No password provided.")
      .min(8, "Password is too short - should be 8 characters minimum."),
  });

  handleLogin = () => {
    const auth = new oasisApi.Auth();
    let data = {
      email: this.state.email,
      password: this.state.password,
    };

    const headers = {
      "Content-Type": "application/json",
    };

    this.setState({ loading: true });
    auth
      .login(data)
      .then((res) => {
        if (res.error) {
          toast.error(res.data.message);
          return;
        }
        toast.success(" Successfully Updated!");
        console.log(res);
        this.setState({ user: res.data.avatar });
      })
      .catch((err) => {
        console.error("There was an error!");
        this.setState({ loading: false });
        toast.error(err.data.message);
      })
      .finally(() => {
        this.setState({ loading: false });
      });
  };

  showForgetPassword = (hideLogin) => {
    this.setState({
      showForgetPassword: true,
    });
    hideLogin();
  };

  hideForgetPassword = () => {
    this.setState({
      showForgetPassword: false,
    });
  };

  render() {
    const { showPassword, loading } = this.state;
    const { show, hide, change } = this.props;

    return (
      <>
        <ToastContainer
          position="top-center"
          autoClose={5000}
          hideProgressBar={false}
          newestOnTop={false}
          closeOnClick
          rtl={false}
          pauseOnFocusLoss
          draggable
          pauseOnHover
        />
        <Formik
          initialValues={this.initialValues}
          validationSchema={this.validationSchema}
          onSubmit={(values, { setSubmitting, resetForm }) => {
            setTimeout(() => {
              this.setState({
                email: values.email,
                password: values.password,
              });
              this.handleLogin();

              setSubmitting(true);
              // resetForm();
              setSubmitting(false);
            }, 400);
          }}
        >
          {({
            values,
            errors,
            touched,
            isSubmitting,
            handleChange,
            handleBlur,
            handleSubmit,
          }) => (
            <Modal centered className="custom-modal" show={show} onHide={hide}>
              <Modal.Body>
                <span className="form-cross-icon" onClick={hide}>
                  <i className="fa fa-times"></i>
                </span>

                <form className="custom-form" onSubmit={handleSubmit}>
                  <div className="form-header">
                    <h2>Log In</h2>

                    <p>
                      Don't have an account?
                      <span className="text-link" onClick={change}>
                        {" "}
                        Sign Up!
                      </span>
                    </p>
                  </div>

                  <div className="form-inputs">
                    <div
                      className={this.handleFormFieldClass(
                        errors.email,
                        touched.email
                      )}
                    >
                      <label>EMAIL</label>
                      <input
                        type="email"
                        name="email"
                        value={values.email}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        placeholder="name@example.com"
                      />
                      <span className="text-danger">
                        {errors.email && touched.email && errors.email}
                      </span>
                    </div>

                    <div
                      className={this.handleFormFieldClass(
                        errors.password,
                        touched.password
                      )}
                    >
                      <label>PASSWORD</label>
                      <div className="have-icon">
                        <input
                          type={`${showPassword ? "text" : "password"}`}
                          name="password"
                          value={values.password}
                          onChange={handleChange}
                          onBlur={handleBlur}
                          placeholder="password"
                        />
                        <img
                          className="field-icon"
                          onClick={() =>
                            this.setState({ showPassword: !showPassword })
                          }
                          src={showPassword ? ShowIcon : HideIcon}
                          alt="icon"
                        />
                      </div>
                      <span className="text-danger">
                        {errors.password && touched.password && errors.password}
                      </span>
                    </div>

                    <div className="forgot-password">
                      <label
                        className="text-link"
                        onClick={() => this.showForgetPassword(hide)}
                      >
                        Forgot Password?
                      </label>
                    </div>

                    <div className="remember-me">
                      <label>
                        <input
                          type="checkbox"
                          name="remember-login"
                          id="remember-login"
                        />
                        Remember me
                      </label>
                    </div>

                    <button
                      type="submit"
                      className="submit-button"
                      disabled={isSubmitting}
                    >
                      {loading ? "Logging in " : "Submit "}{" "}
                      {loading ? (
                        <Loader
                          type="Oval"
                          height={15}
                          width={15}
                          color="#fff"
                        />
                      ) : null}
                    </button>
                  </div>
                </form>
              </Modal.Body>
            </Modal>
          )}
        </Formik>

        <ForgetPassword
          className="custom-form"
          show={this.state.showForgetPassword}
          hide={this.hideForgetPassword}
          change={this.showForgetPassword}
        />
      </>
    );
  }

  handleFormFieldClass(error, touched) {
    let classes = "single-form-field ";
    classes += error && touched ? "has-error" : "";

    return classes;
  }
}
