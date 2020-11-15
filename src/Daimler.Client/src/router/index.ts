import Vue from "vue";
import VueRouter from "vue-router";
import UrlRepository from "@/repo/url-repo";

Vue.use(VueRouter);

const router = new VueRouter({
  mode: "history",
  scrollBehavior() {
    return window.scrollTo({ top: 0, behavior: "smooth" });
  },
  routes: [
    {
      path: "/",
      name: "home",
      component: () => import("@/view/Home.vue"),
      beforeEnter: (to, from, next) => {
        return next();
      }
    },
    {
      path: "/:code",
      component: {
        template: "<router-view></router-view>",
      },
      beforeEnter: async (to, from, next) => {
        var url = await UrlRepository.navigate(to.params.code);
        window.location.href = url as string;
      }
    },
    {
      path: "*",
      name: "error",
      component: () => import("@/view/Error.vue"),
      beforeEnter: (to, from, next) => {
        debugger;
        return next();
      }
    },
  ],
});

router.beforeEach((to, from, next) => {
  next();
});
router.afterEach(() => { });

export default router;
