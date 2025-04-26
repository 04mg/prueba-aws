# Stage 1: Build the Angular app
FROM node:16 AS builder
WORKDIR /app
COPY package*.json ./

# Try using a different npm mirror and add retry logic
RUN npm config set registry https://registry.npmmirror.com/ \
    && npm config set fetch-retry-maxtimeout 600000 \
    && npm config set fetch-retry-mintimeout 10000 \
    && npm config set fetch-retries 5 \
    && npm install --no-audit --no-fund

COPY . .
RUN npm run build --prod

# Stage 2: Serve the Angular app using Nginx
FROM nginx:alpine
COPY --from=builder /app/dist/pharma-go /usr/share/nginx/html
# Copy custom Nginx configuration file
COPY nginx.conf /etc/nginx/conf.d/default.conf
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
