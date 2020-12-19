import {Subscription} from 'apollo-angular';
import gql from 'graphql-tag';

export const GAME_SUSCRIPTION = gql`
  subscription{
    gameReceived{
      id
      name
      startTime
      link
      playersNumber
      gettedNumbers
      finished
    }
  }
`;