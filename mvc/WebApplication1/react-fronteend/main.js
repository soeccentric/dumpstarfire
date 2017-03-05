import React from 'react'
import ReactDOM from 'react-dom'
import something from './a'

var element = (
  <div>something</div>
);
console.log(something.a());

var mountNode = document.getElementById('app');
console.log(mountNode);
ReactDOM.render(element, mountNode);
