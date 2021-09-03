import React, { Component } from "react";
import "../CSS/Login.css";
import { signup } from "../actions/auth";
import { connect } from "react-redux";

class Signup extends Component {
  constructor(props) {
    super(props);

    this.state = {
      email: "",
      password: "",
      confirm_password: "",
    };
  }

  handleSignup = (e) => {
    const dispatch = this.props.dispatch;
    e.preventDefault();

    if (this.state.password === this.state.confirm_password) {
      let data = {
        email: this.state.email,
        password: this.state.password,
      };

      dispatch(signup(data));
    }
  };

  handleEmailChange = (event) => {
    this.setState({ email: event.target.value });
  };

  handlePasswordChange = (event) => {
    this.setState({ password: event.target.value });
  };

  handleConfirmPasswordChange = (event) => {
    this.setState({ password: event.target.value });
  };

  render() {
    return (
      <form className="login-form" onSubmit={this.handleSignup}>
        <div className="login-title">
          <h1 className="login-header">Sign Up</h1>
          <p className="login-title-text">
            Already have an account?
            <span onClick={this.props.change} className="link">
              Log In!
            </span>
          </p>
        </div>

        <div className="login-inputs">
          <label htmlFor="login-email">EMAIL</label>
            {" "}
            <input
              value={this.state.email}
              onChange={this.handleEmailChange}
              type="email"
              placeholder="name@example.com"
            />
          <label htmlFor="login-password">PASSWORD</label>
          <input
            type="password"
            value={this.state.password}
            onChange={this.handlePasswordChange}
          />

          <label htmlFor="confirm-signup-password">Confirm Password</label>
          <input
            type="password"
            value={this.state.confirm_password}
            onChange={this.handleConfirmPasswordChange}
          />

          <div>
            <input type="checkbox" name="accept-terms" id="accept-terms" />
            <label for="accept-terms">
              I have read and accept the
              <a href="#0" className="link">
                Terms of Service
              </a>
            </label>
          </div>

          <button type="submit" className="login-submit">
            Submit
          </button>
        </div>
      </form>
    );
  }
}

export default connect()(Signup);
