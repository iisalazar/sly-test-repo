// @ts-check
import React, { createContext, useMemo, useState, useContext } from 'react';
import { setCookie } from 'cookies-next';

/**
 * @typedef {Object} UserInfo
 * @property {string} username
 * @property {string} email
 * @property {string} role
 */


const initialValues = {
  /**
   * @type {UserInfo}
   */
  userInfo: {
    username: '',
    email: '',
    role: '',
  },
  accessToken: '',
  expiresIn: -1,
  /**
   * 
   * @param {UserInfo} userInfo 
   */
  setUserInfo: (userInfo) => { },
  /**
   * 
   * @param {string} accessToken 
   * @param {number} expireIn
   */
  setAccessToken: (accessToken, expireIn) => { },

}

export const AuthContext = createContext(initialValues);


export function AuthProvider({ children }) {
  const [userInfo, setUserInfo] = useState(initialValues.userInfo);
  const [accessToken, setAccessToken] = useState(initialValues.accessToken);
  const [expiresIn, setExpiresIn] = useState(initialValues.expiresIn);

  function _setUserInfo(userInfo) {
    setUserInfo(userInfo);
  }

  /**
   * 
   * @param {string} accessToken 
   * @param {number} expiresIn In milliseconds
   */
  function _setAccessToken(accessToken, expiresIn) {
    // store access token in cookies
    // it's fine though since the access token is short lived
    setCookie("accessToken", accessToken, {
      expires: new Date(Date.now() + expiresIn),
      path: '/',
    });

    setAccessToken(accessToken);
    setExpiresIn(expiresIn);
  }

  const contextValue = useMemo(() => {
    return {
      userInfo,
      accessToken,
      expiresIn,
      setUserInfo: _setUserInfo,
      setAccessToken: _setAccessToken,
    }
  }, [accessToken, expiresIn, userInfo])

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  )
}

export function useAuthProvider() {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error('useAuth must be used within a AuthProvider');
  }
  return context;
}