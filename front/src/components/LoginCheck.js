import {useEffect, useState } from 'react';
import axios from 'axios';
import {ApiBaseUrl} from "../App";

async function ValidateToken() {
  const token = localStorage.getItem('token');

  try {
    const response = await axios.get(ApiBaseUrl+'auth/validate_token/', {
      headers: {
        Authorization: "bearer "+ token,
      },
    });

    return true;
  } catch (error) {
    console.error(error);
    return false;
  }
}

export default function LoginCheck() {
  const [loggedIn, setLoggedIn] = useState(null); // Используем null в качестве начального состояния

  useEffect(() => {
    if (localStorage.getItem('token') && localStorage.getItem('username')) {
      // Выполняем проверку только если есть токен и имя пользователя в локальном хранилище
      ValidateToken().then((res) => {
        setLoggedIn(res);
      });
    }
    else{
      setLoggedIn(false);
    }
  }, []); // Пустой массив зависимостей - useEffect будет вызван только один раз
  return loggedIn; // Возвращаем результат проверки
}
