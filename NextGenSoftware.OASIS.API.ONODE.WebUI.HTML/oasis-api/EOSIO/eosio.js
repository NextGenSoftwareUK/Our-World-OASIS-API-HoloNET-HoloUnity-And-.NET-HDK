import axios from "axios";
import Avatar from "../avatar/avatar";

class EOSIO {
  constructor() {
    this.avatar = new Avatar();
  }

  async getEOSIOAccountNameForAvatar(id = "") {
    this.token = await this.avatar.callLogin();
    if (this.token === -1)
      return { error: true, data: { message: "You are not logged in!" } };
    const config = {
      method: "get",
      url: `https://api.oasisplatform.world/api/eosio/GetEOSIOAccountNameForAvatar?avatarId=${id}`,
      headers: {
        Authorization: `Bearer ${this.token.jwtToken}`,
      },
    };

    return axios(config)
      .then(function (response) {
        if (response.data.isError) return { error: true, data: response.data };
        else return { error: false, data: response.data };
      })
      .catch(function (error) {
        return { error: true, data: error };
      });
  }

  async getEOSIOAccountPrivateKeyForAvatar(id = "") {
    this.token = await this.avatar.callLogin();
    if (this.token === -1)
      return { error: true, data: { message: "You are not logged in!" } };
    const config = {
      method: "get",
      url: `https://api.oasisplatform.world/api/eosio/GetEOSIOAccountPrivateKeyForAvatar?avatarId=${id}`,
      headers: {
        Authorization: `Bearer ${this.token.jwtToken}`,
      },
    };

    return axios(config)
      .then(function (response) {
        if (response.data.isError) return { error: true, data: response.data };
        else return { error: false, data: response.data };
      })
      .catch(function (error) {
        return { error: true, data: error };
      });
  }

  async getEOSIOAccountForAvatar(id) {
    this.token = await this.avatar.callLogin();
    if (this.token === -1)
      return { error: true, data: { message: "You are not logged in!" } };
    const config = {
      method: "get",
      url: `https://api.oasisplatform.world/api/eosio/GetEOSIOAccountForAvatar?avatarId=${id}`,
      headers: {
        Authorization: `Bearer ${this.token.jwtToken}`,
      },
    };

    return axios(config)
      .then(function (response) {
        if (response.data.isError) return { error: true, data: response.data };
        else return { error: false, data: response.data };
      })
      .catch(function (error) {
        return { error: true, data: error };
      });
  }

  async getBalanceForEOSIOAccount(
    data = { accountName: "", code: "", symbol: "" }
  ) {
    this.token = await this.avatar.callLogin();
    if (this.token === -1)
      return { error: true, data: { message: "You are not logged in!" } };
    const config = {
      method: "get",
      url: `https://api.oasisplatform.world/api/eosio/getBalanceForEOSIOAccount?eosioAccountName=${data.accountName}&code=${data.code}&symbol=${data.symbol}`,
      headers: {
        Authorization: `Bearer ${this.token.jwtToken}`,
      },
    };

    return axios(config)
      .then(function (response) {
        if (response.data.isError) return { error: true, data: response.data };
        else return { error: false, data: response.data };
      })
      .catch(function (error) {
        return { error: true, data: error };
      });
  }

  async linkAccountToAvatar(data = { avatarId, accountName }) {
    this.token = await this.avatar.callLogin();
    if (this.token === -1)
      return { error: true, data: { message: "You are not logged in!" } };

    const config = {
      method: "post",
      url: `https://api.oasisplatform.world/api/eosio/${data.avatarId}/${data.accountName}`,
      headers: {
        Authorization: `Bearer ${this.token.jwtToken}`,
      },
    };

    return axios(config)
      .then(function (response) {
        if (response.data.isError) return { error: true, data: response.data };
        else return { error: false, data: response.data };
      })
      .catch(function (error) {
        return { error: true, data: error };
      });
  }
}

export default EOSIO;
