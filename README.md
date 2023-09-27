# RedditRTS

## Setup ApiKey

The apikey is stored in appsettings.json in the RedditRTS.Api project
Application Only api key

{
  "Reddit": {
    "ApiKey": "put key here"
  }
]

You can also put the above in secrets on your machine

## General Reddit configurations

  ApiKey - The api key
  ApiHost - The reddit host to connect to get posts
  Subreddits - An array of full r/subreddits 
  RateLimitWaitTimeNoheader - The default time to wait inbetween calls if there are no rate limiting headers
  MaximumRequestLimit - The amount of posts to get in one api call, used by the worker

  Example:
  {
      "ApiKey": "replace_me",
      "ApiHost": "https://oauth.reddit.com",
      "Subreddits": [ "r/Funny", "r/Home" ],
      "RateLimitWaitTimeNoHeaders": 10,
      "MaximumRequestLimit": 100
    }
}

## Endpoints

### Posts

### GET [host]/posts/mostupvotes - Gets a list of the my up voted posts, ordered descending by up votes
query parameters:
subreddit - Optional, will return the top up voted posts for a subreddit.  ex. Funny, not r/Funny
limit - Number of up voted posts to return, defaults to 10, maximum 100

### GET [host]/posts/authors/mostposts - Gets a list of the authors with the most posts in a subreddit
query parameters:
subreddit - Optional, will return the top up voted posts for a subreddit.  ex. Funny, not r/Funny
limit - Number of up voted posts to return, defaults to 10, maximum 100
