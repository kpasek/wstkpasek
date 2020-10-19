import React, { Component } from "react";

export class User extends Component {
  static displayName = User.name;

  constructor(props) {
    super(props);
    this.state = {};
    //bind User object to handleLoginBtn function
    //after that we can use this inside function
    //other solution is using arrow function name = () => {}
    this.handleLoginBtn = this.handleLoginBtn.bind(this);
  }

  componentDidMount() {}

  handleLoginBtn = () => {
    const login = document.getElementById("login-email").value;
    const password = document.getElementById("login-password").value;
    fetch("/api/user/login", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        Email: login,
        Password: password,
      }),
    }).then((response) => {
      if (response.ok) {
        console.log("login successful, start redirect");
        window.location.href = "/";
      }
    });
  };

  handleRegister = () => {
    const login = document.getElementById("reg-email").value;
    const password = document.getElementById("reg-password").value;
    const name = document.getElementById("reg-name").value;
    const lastname = document.getElementById("reg-lastname").value;
    const gender = document.getElementById("reg-gender").value;
    const birthdate = document.getElementById("reg-birthdate").value;

    fetch("/api/user/register", {
      method: "POST",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        Email: login,
        Password: password,
        Name: name,
        LastName: lastname,
        Gender: gender,
        Birthdate: birthdate,
      }),
    }).then((response) => {
      if (response.ok) {
        console.log("register successful, start redirect");
        window.location.href = "/";
      }
    });
  };
  render() {
    return (
      <div className="row">
        <div className="col-md-4 mr-auto ml-auto mb-4">
          <div className="section-body p-1 p-lg-3">
            <div className="section-title">Zaloguj się</div>
            <div id="login-form">
              <div className="form-check p-0 mb-3">
                <label htmlFor="login-email">Email</label>
                <input
                  type="email"
                  className="form-control"
                  id="login-email"
                  name="Email"
                  aria-describedby="emailHelp"
                  placeholder="Email"
                  required
                />
              </div>
              <div className="form-check p-0 mb-3">
                <label htmlFor="login-password">Password</label>
                <input
                  type="password"
                  className="form-control"
                  id="login-password"
                  name="Password"
                  placeholder="Hasło"
                  required
                />
              </div>
              <button
                className="btn btn-primary float-right mt-3"
                onClick={this.handleLoginBtn}
              >
                Zaloguj
              </button>
              {/* <div style="clear: both;"></div> */}
            </div>
          </div>
        </div>

        <div className="col-md-8 mr-auto ml-auto mb-4">
          <div className="section-body p-1 p-lg-3">
            <div className="section-title">Zarejestruj się</div>
            <div className="needs-validation" id="reg-form" noValidate>
              <div className="form-row">
                <div className="col-md-6 mb-3">
                  <label htmlFor="reg-email">Email</label>
                  <div className="input-group">
                    <div className="input-group-prepend">
                      <span className="input-group-text" id="validation-email">
                        &#64;
                      </span>
                    </div>
                    <input
                      type="email"
                      className="form-control"
                      id="reg-email"
                      name="Email"
                      placeholder="Email"
                      aria-describedby="validation-email"
                      required
                    />
                    <div className="invalid-tooltip">
                      Email musi być unikalny oraz prawidłowy.
                    </div>
                  </div>
                </div>

                <div className="col-md-6 mb-3">
                  <label htmlFor="reg-password">Hasło</label>
                  <input
                    type="password"
                    className="form-control"
                    id="reg-password"
                    name="Password"
                    placeholder="Hasło"
                    required
                  />
                  <div className="invalid-tooltip">Musisz podać hasło.</div>
                </div>
              </div>
              <div className="form-row">
                <div className="col-md-6 mb-3">
                  <label htmlFor="reg-name">Imię</label>
                  <input
                    type="text"
                    className="form-control"
                    id="reg-name"
                    name="Name"
                    placeholder="Imię"
                  />
                  <div className="valid-tooltip">Jest OK!</div>
                </div>
                <div className="col-md-6 mb-3">
                  <label htmlFor="reg-lastname">Nazwisko</label>
                  <input
                    type="text"
                    className="form-control"
                    id="reg-lastname"
                    name="LastName"
                    placeholder="Nazwisko"
                  />
                  <div className="valid-tooltip">Jest OK!</div>
                </div>
              </div>
              <div className="form-row">
                <div className="col-md-6 mb-3">
                  <label className="mr-2" htmlFor="reg-gender">
                    Płeć
                  </label>
                  <select
                    className="custom-select mr-sm-2"
                    id="reg-gender"
                    name="Gender"
                    required
                  >
                    <option defaultValue>Wybierz</option>
                    <option value="K">Kobieta</option>
                    <option value="M">Mężczyzna</option>
                    <option value="I">Inne</option>
                  </select>
                  <div className="invalid-tooltip">Proszę wybrać płeć.</div>
                </div>
                <div className="col-md-6 mb-3">
                  <label htmlFor="reg-birthdate">Data urodzenia</label>
                  <input
                    type="date"
                    className="form-control"
                    id="reg-birthdate"
                    name="Birthday"
                    placeholder="Data urodzenia"
                  />
                  <div className="invalid-tooltip">
                    Proszę podać datę urodzenia.
                  </div>
                </div>
              </div>
              <button
                className="btn btn-primary float-right"
                onClick={this.handleRegister}
              >
                Rejestruj
              </button>
              {/* <div style="clear: both;"></div> */}
            </div>
          </div>
        </div>
      </div>
    );
  }
}
