import React from "react";
import { ToastContainer, toast } from "react-toastify";

import { Modal } from "react-bootstrap";
import Loader from "react-loader-spinner";
import { Formik } from "formik";
import * as Yup from "yup";
import oasisApi from "oasis-api";
const axios = require("axios");

export default class ForgetPassword extends React.Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      loading: false,
      user: null,
    };
  }

  initialValues = {
    email: "",
  };
  validationSchema = Yup.object().shape({
    email: Yup.string().email("Email is invalid").required("Email is required"),
  });

  handleForget = () => {
    let data = {
      email: this.state.email,
    };

    const auth = new oasisApi.Auth();

    this.setState({ loading: true });

    auth
      .forgotPassword(data)
      .then((res) => {
        if (res.data.isError) {
          toast.error(res.data.message);
          return;
        }
        toast.success(res.data.message);
        this.props.hide();
      })
      .catch((err) => {
        toast.error(err.data.message);
      })
      .finally(() => {
        this.setState({ loading: false });
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
              });
              this.handleForget();

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
                    <h2>Forget Password</h2>
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
                        disabled={loading}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        placeholder="name@example.com"
                      />
                      <span className="text-danger">
                        {errors.email && touched.email && errors.email}
                      </span>
                    </div>

                    <button
                      type="submit"
                      className="submit-button"
                      disabled={isSubmitting}
                    >
                      {loading ? "Sending Link" : "Submit "}{" "}
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
      </>
    );
  }

  handleFormFieldClass(error, touched) {
    let classes = "single-form-field ";
    classes += error && touched ? "has-error" : "";

    return classes;
  }
}
