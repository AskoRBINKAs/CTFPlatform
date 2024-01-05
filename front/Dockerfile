FROM node:15.13-alpine
EXPOSE 3000
WORKDIR /app
COPY . .
RUN npm i
RUN npm run build
CMD ["npm","start"]