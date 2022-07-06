import React, { Component } from "react";
import { Variables } from "./Components/Variables";
import { useNavigate } from "react-router-dom";

class LoginClass extends Component {
  constructor(props) {
    super(props);
    this.state = {
      Email: "",
      Password: "",
    };

    this.changeEmail = this.changeEmail.bind(this);
    this.changePassword = this.changePassword.bind(this);
    this.Login = this.Login.bind(this);
  }
  changeEmail(event) {
    this.setState({ Email: event.target.value });
  }
  changePassword(event) {
    this.setState({ Password: event.target.value });
  }
  Login() {
    if (this.state.Email === "" || this.state.Password === "") {
      alert("Some fild is empty, please enter information and try again.");
      return;
    }
    fetch(Variables.API_URL + "/User/login", {
      method: "POST",
      headers: {
        accept: "application/json",
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        email: this.state.Email,
        password: this.state.Password,
      }),
    })
      .then((res) => {
        if (res.status === 200) {
          console.log(res.status);
          return res.json();
        } else if (res.status === 400) {
          alert("Wrong pasword or email , please try again.");
          return;
        } else {
          alert("Something go wrong, please try again later.");
          return;
        }
      })
      .then((value) => {
        sessionStorage.setItem("access_token", value.access_token);
        sessionStorage.setItem("Email", value.email);
        sessionStorage.setItem("Id", value.userId);
        sessionStorage.setItem("Role", value.role);
        alert("You are logged");
        this.props.navigation("/");
        window.location.reload(false);
      })
  }

  render() {
    const { Email, Password } = this.state;
    return (
      <article>
        <div className="loginForm">
          <div className="inputDiv">
            <span>Email: </span>
            <input
              className="input"
              value={Email}
              onChange={this.changeEmail}
              placeholder="Your email"
            ></input>
          </div>
          <div className="inputDiv">
            <span>Password: </span>
            <input
              className="input"
              type="text"
              value={Password}
              onChange={this.changePassword}
              placeholder="Your password"
            ></input>
          </div>
          <button className="simpleButton" onClick={this.Login}>
            Login
          </button>
        </div>
      </article>
    );
  }
}

export default function Login(props) {
  const navigation = useNavigate();

  return <LoginClass {...props} navigation={navigation} />;
}
