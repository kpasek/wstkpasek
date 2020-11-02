import React, { Component } from "react";

export class Account extends Component {
  static displayName = Account.name;

  constructor(props) {
    super(props);
    this.state = { profile: [], loading: true };
  }

  componentDidMount = async () => {
    const profileResponse = await fetch("api/profiles", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const settingsResponse = await fetch("api/settings", {
      method: "GET",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
    });
    const profile = await profileResponse.json();
    const settings = await settingsResponse.json();
    this.setState({
      profile: profile,
      settings: settings,
      loading: false,
    });
  };
  setTime() {
    let result = "";
    if (this.state.settings.trainingHour < 10) {
      result = "0" + this.state.settings.trainingHour;
    } else {
      result = this.state.settings.trainingHour;
    }
    result += ":";
    if (this.state.settings.trainingMinute < 10) {
      result += "0" + this.state.settings.trainingMinute;
    } else {
      result += this.state.settings.trainingMinute;
    }
    return result;
  }
  handleUpdateProfile = async () => {
    const name = document.getElementById("profile-name").value;
    const lastName = document.getElementById("profile-lastname").value;
    const gender = document.getElementById("profile-gender").value;
    const birthday = document.getElementById("profile-birthday").value;

    await fetch("api/profiles", {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        profileId: this.state.profile.profileId,
        name: name,
        lastName: lastName,
        gender: gender,
        birthday: birthday,
      }),
    });
    const newProfile = {
      profileId: this.state.profile.profileId,
      name: name,
      lastName: lastName,
      gender: gender,
      birthday: birthday,
    };
    this.setState({
      profile: newProfile,
      updateProfileMessage: "Profil został zaktualizowany.",
    });
  };
  handleUpdateSettings = async () => {
    const trainingTime = document.getElementById("settings-time").value;
    const trainingDayInterval = document.getElementById(
      "settings-trainingDayInterval"
    ).value;
    await fetch("api/settings", {
      method: "PUT",
      mode: "cors",
      cache: "no-cache",
      credentials: "include",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        settingsId: this.state.settings.settingsId,
        trainingHour: parseInt(trainingTime.substr(0, 2)),
        trainingMinute: parseInt(trainingTime.substr(3, 2)),
        trainingDayInterval: parseInt(trainingDayInterval),
      }),
    });
    const newSettings = {
      settingsId: this.state.settings.settingsId,
      trainingHour: parseInt(trainingTime.substr(0, 2)),
      trainingMinute: parseInt(trainingTime.substr(3, 2)),
      trainingDayInterval: parseInt(trainingDayInterval),
    };
    this.setState({
      settings: newSettings,
      updateSettingsMessage: "Ustawienia zostały zapisane.",
    });
  };
  handleCloseMessage = () => {
    this.setState({
      updateProfileMessage: "",
      updateSettingsMessage: "",
    });
  };
  renderUpdateMessage() {
    if (this.state.updateProfileMessage) {
      return (
        <React.Fragment>
          <div className="alert alert-success" role="alert">
            {this.state.updateProfileMessage}
            <span className="float-right">
              <i className="icon-cancel" onClick={this.handleCloseMessage} />
            </span>
          </div>
        </React.Fragment>
      );
    }
  }
  renderSettingsMessage() {
    if (this.state.updateSettingsMessage) {
      return (
        <React.Fragment>
          <div className="alert alert-success" role="alert">
            {this.state.updateSettingsMessage}
            <span className="float-right">
              <i className="icon-cancel" onClick={this.handleCloseMessage} />
            </span>
          </div>
        </React.Fragment>
      );
    }
  }
  renderBody() {
    if (this.state.loading) {
      return <h3>Trwa ładowanie</h3>;
    } else {
      return (
        <React.Fragment>
          <div className="mt-3 col-lg-4">
            {this.renderUpdateMessage()}
            <h2>Profil</h2>
            <div className="">
              <label htmlFor="profile-name">Imię</label>
              <input
                className="form-control"
                id="profile-name"
                type="text"
                defaultValue={this.state.profile.name}
              />
              <label htmlFor="profile-lastname">Nazwisko</label>
              <input
                className="form-control"
                id="profile-lastname"
                type="text"
                defaultValue={this.state.profile.lastName}
              />
              <label htmlFor="profile-gender">Płeć</label>
              <select
                className="form-control"
                id="profile-gender"
                type="text"
                defaultValue={this.state.profile.gender}
              >
                <option value="K">Kobieta</option>
                <option value="M">Mężczyzna</option>
                <option value="I">Inne</option>
              </select>
              <label htmlFor="profile-birthday">Data urodzenia</label>
              <input
                className="form-control"
                id="profile-birthday"
                type="date"
                defaultValue={this.state.profile.birthday.substr(0, 10)}
              />
              <button
                className="btn btn-outline-success mt-2 float-right"
                onClick={this.handleUpdateProfile}
              >
                Zatwierdź
              </button>
              <div className="clear-fix"></div>
            </div>
          </div>
          <div className="col-lg-4 mt-3">
            {this.renderSettingsMessage()}
            <h2>Ustawienia aplikacji</h2>
            <label htmlFor="settings-time">Domyślna godzina treningu</label>
            <input
              className="form-control"
              id="settings-time"
              type="time"
              defaultValue={this.setTime()}
            />
            <label htmlFor="settings-trainingDayInterval">
              Ilość dni przerwy między treningami
            </label>
            <input
              className="form-control"
              id="settings-trainingDayInterval"
              min="0"
              type="number"
              defaultValue={parseInt(this.state.settings.trainingDayInterval)}
            />
            <button
              className="btn btn-outline-success mt-2"
              onClick={this.handleUpdateSettings}
            >
              Zatwierdź
            </button>
          </div>
        </React.Fragment>
      );
    }
  }
  render() {
    return this.renderBody();
  }
}
