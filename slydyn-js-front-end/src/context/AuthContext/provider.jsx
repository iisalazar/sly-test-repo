// @ts-check
import React, { createContext, useMemo, useState, useContext } from 'react';

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
   * @type {React.Dispatch<React.SetStateAction<UserInfo>>}
   */
  setUserInfo: (userInfo) => { },
  /**
   * @type {React.Dispatch<React.SetStateAction<string>>}
   */
  setAccessToken: (accessToken) => { },

  /**
   * @type {React.Dispatch<React.SetStateAction<number>>}
   */

  setExpiresIn: (expiresIn) => { },
}

export const AuthContext = createContext(initialValues);


export function AuthProvider({ children }) {
  const [userInfo, setUserInfo] = useState(initialValues.userInfo);
  const [accessToken, setAccessToken] = useState(initialValues.accessToken);
  const [expiresIn, setExpiresIn] = useState(initialValues.expiresIn);

  const contextValue = useMemo(() => {
    return {
      userInfo,
      accessToken,
      expiresIn,
      setUserInfo,
      setAccessToken,
      setExpiresIn,
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